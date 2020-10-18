using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;

namespace demogit
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri orgUrl = new Uri("https://dev.azure.com/SincroPrueba/"); // Organization URL
            String personalAccessToken = "t32o247kqksqhohqin7zdxmkluqnbaba6ljher5guwdihflfx7kq";  // personalAccessToken
            int workItemId = 2; // ID of a work Item, for example: 12

            // Create a connection
            VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, personalAccessToken));

            // Show details a work item
            ShowWorkItemDetails(connection, workItemId).Wait();

            Console.WriteLine("1er commit");
            Console.WriteLine("2do commit"); 
        }

        static private async Task ShowWorkItemDetails(VssConnection connection, int workItemId)
        {
            // Get an instance of the work item tracking client
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                // Get the specified work item
                WorkItem workitem = await witClient.GetWorkItemAsync(workItemId);

                //Output the work item's field values
                foreach (var field in workitem.Fields)
                {
                    Console.WriteLine("{0}: {1}", field.Key, field.Value);
                }
            }
            catch (AggregateException aex)
            {
                VssServiceException vssex = aex.InnerException as VssServiceException;
                if (vssex != null)
                {
                    Console.WriteLine(vssex.Message);
                }
            }

        }
    }
}
