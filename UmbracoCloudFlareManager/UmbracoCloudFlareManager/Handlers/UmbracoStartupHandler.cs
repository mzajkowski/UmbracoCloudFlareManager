using System;
using System.Web.Hosting;
using System.Xml;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Web;
using UmbracoCloudFlareManager.Helpers;

namespace UmbracoCloudFlareManager.Handlers
{
    public class UmbracoStartupHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            AddPluginSectionToDevelopersDashboard();

            ContentService.Published += ContentService_Published;
        }

        private void ContentService_Published(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            //// Check if PurgeHomepage setting is set
            var useCloudFlare = Config.GetSettingValue("EnableCloudflare");
            if (useCloudFlare == null || !(bool)useCloudFlare) return;

            if ((bool) useCloudFlare)
            {
                foreach (var entity in e.PublishedEntities)
                {
                    Purge(entity.Id);

                    //// Check if PurgeHomepage setting is set
                    var purgeHomepage = Config.GetSettingValue("PurgeHomepage");
                    if (purgeHomepage == null || !(bool) purgeHomepage) continue;

                    //// Get Homepage node
                    var homepage = Config.GetSettingValue("Homepage");
                    int homepageNodeId;

                    if (homepage != null && int.TryParse(homepage.ToString(), out homepageNodeId))
                    {
                        Purge(homepageNodeId);
                    }
                }
            }
        }

        private void AddPluginSectionToDevelopersDashboard()
        {
            //Open up dashboard config file
            const string dashboardPath = "~/config/dashboard.config";

            //Path to the file resolved
            var dashboardFilePath = HostingEnvironment.MapPath(dashboardPath);

            //Load dashboard.config XML file
            var dashboardXml = new XmlDocument();

            try
            {
                if (dashboardFilePath == null) return;

                dashboardXml.Load(dashboardFilePath);

                // Section Node
                var findSection = dashboardXml.SelectSingleNode("//section [@alias='StartupDeveloperDashboardSection']");

                // Couldn't find it
                if (findSection == null)
                {
                    // Developers section is not found - something is really bad!
                    return;
                }
                else
                {

                    // Check if our dashboard is already added
                    XmlNode customTab = findSection.SelectSingleNode("//tab [@caption='CloudFlare Manager']");

                    if (customTab == null)
                    {
                        var xmlToAdd = "<tab caption='CloudFlare Manager'>" +
                                       "<control addPanel='true' panelCaption=''>/App_Plugins/UmbracoCloudFlareManager/backoffice/views/dashboard.html</control>" +
                                       "</tab>";

                        // Load in the XML string above
                        var xmlNodeToAdd = new XmlDocument();
                        xmlNodeToAdd.LoadXml(xmlToAdd);

                        // Append the xml above to the dashboard node
                        try
                        {
                            if (xmlNodeToAdd.DocumentElement != null)
                            {
                                var copiedNode = dashboardXml.ImportNode(xmlNodeToAdd.DocumentElement, true);
                                findSection.AppendChild(copiedNode);
                            }
                            // Save the file flag to true
                            dashboardXml.Save(dashboardFilePath);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error<UmbracoStartupHandler>("Couldn't add content section dashboard", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<UmbracoStartupHandler>("Couldn't add content section dashboard", ex);
            }
        }

        private void Purge(int nodeId)
        {
            //// Get current configured domain
            var currentDomain = Config.GetSettingValue("Domain");
            if (currentDomain != null && !string.IsNullOrWhiteSpace(currentDomain.ToString()))
            {
                //// Get the context
                var context = UmbracoContext.Current.UrlProvider;

                //// Purge Url of entity
                CloudFlareApiHelper.Instance.PurgeFileCache(currentDomain.ToString(), context.GetUrl(nodeId));

                //// Purge additional Urls of entity
                foreach (var additionalUrl in context.GetOtherUrls(nodeId))
                {
                    CloudFlareApiHelper.Instance.PurgeFileCache(currentDomain.ToString(), additionalUrl);
                }
            }
        }
    }
}