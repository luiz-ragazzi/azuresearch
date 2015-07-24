using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using UhyIntranet.Common;

namespace SimpleSearchMVCApp
{
    public class FeaturesSearch
    {
        private static SearchServiceClient _searchClient;
        private static SearchIndexClient _indexClient;

        public static string errorMessage;

        public FeaturesSearch(string s)
        {
            
        }

        static FeaturesSearch()
        {
            try
            {
                string searchServiceName = ConfigurationManager.AppSettings["SearchServiceName"];
                string apiKey = ConfigurationManager.AppSettings["SearchServiceApiKey"];


                // Create an HTTP reference to the catalog index
                _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
                _indexClient = _searchClient.Indexes.GetClient("intranetnews");
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }



        public DocumentSearchResponse Search(string searchText, ODataFilter filter)
        {
            // Execute search based on query string
            try
            {   
                var sp = new SearchParameters() { SearchMode = SearchMode.All };
                sp.Filter = filter.GetFilter();
                return _indexClient.Documents.Search(searchText, sp);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error querying index: {0}\r\n", ex.Message);
            }
            return null;
        }

    }
}