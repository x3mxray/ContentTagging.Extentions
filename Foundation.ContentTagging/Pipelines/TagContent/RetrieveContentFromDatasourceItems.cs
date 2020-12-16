using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.ContentTagging.Extentions;
using Sitecore.Data.Items;
using Sitecore.ContentTagging.Core.Models;
using Sitecore.ContentTagging.Core.Providers;
using Sitecore.ContentTagging.Pipelines.TagContent;

namespace Foundation.ContentTagging.Pipelines.TagContent
{
    public class RetrieveContentFromDatasourceItems
    {
        public void Process(TagContentArgs args)
        {
            var taggableContentList = new List<TaggableContent>();
            if (args.Content != null && args.Content.Any())
            {
                taggableContentList.AddRange(args.Content);
            }

            foreach (IContentProvider<Item> contentProvider in args.Configuration.ContentProviders)
            {
                var dataSoutceItems = args.ContentItem.GetDataSourceItems();
                foreach (var item in dataSoutceItems)
                {
                    var content = (StringContent)contentProvider.GetContent(item);

                    if (!string.IsNullOrEmpty(content.Content) && !string.IsNullOrEmpty(content.Content.Trim()))
                    {
                        taggableContentList.Add(content);
                    }
                }

            }
            args.Content = taggableContentList;
        }
    }
}
