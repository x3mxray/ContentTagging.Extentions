using System;
using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Layouts;

namespace Foundation.ContentTagging.Extentions
{
    public static  class ItemExtentions
    {
        public static List<Item> GetDataSourceItems(this Item i)
        {
            List<Item> list = new List<Item>();
            foreach (var reference in i.GetRenderingReferences())
            {
                Item dataSourceItem = reference.GetDataSourceItem();
                if (dataSourceItem != null)
                {
                    list.Add(dataSourceItem);
                }
            }
            return list;
        }

        public static Item GetDataSourceItem(this RenderingReference reference)
        {
            return reference != null ? GetDataSourceItem(reference.Settings.DataSource, reference.Database) : null;
        }

        private static Item GetDataSourceItem(string id, Database db)
        {
            return Guid.TryParse(id, out var itemId)
                ? db.GetItem(new ID(itemId))
                : db.GetItem(id);
        }

        public static RenderingReference[] GetRenderingReferences(this Item i)
        {
            if (i == null)
            {
                return new RenderingReference[0];
            }
            var defaultDeviceItem = i.Database.Resources.Devices[new ID("{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}")];
            return i.Visualization.GetRenderings(defaultDeviceItem, false);
        }
    }
}
