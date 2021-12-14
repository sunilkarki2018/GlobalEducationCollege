//using GlobalCollege.Entity;
//using GlobalCollege.Entity.DTO;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IO;
//using System.Linq;
//using System.Web;

//namespace GlobalCollege.Admin.Utility
//{
//    public static class HTMLPageGenerator
//    {
//        static string _dynamicLayoutPath = ConfigurationManager.AppSettings["DynamicLayoutPath"];
//        static string _dynamicPagePath = ConfigurationManager.AppSettings["DynamicPagePath"];
//        static string _dynamicLayoutVirtualPath = ConfigurationManager.AppSettings["DynamicLayoutVirtualPath"];
//        public static void GenerateLayout(LayoutSetup layout)
//        {
//            string _layout = @"<!DOCTYPE html>
//<html>
//<head>" + Environment.NewLine;

//            var metaComponents = layout.LayoutComponentSetups.Where(c => c.ComponentType == "header").ToList();

//            metaComponents.OrderBy(o => o.PlacementOrder).ToList().ForEach(metaComponent =>
//            {
//                if (metaComponents.Where(w => w.ParentLayoutComponentSetupId == metaComponent.Id).Count() == 0)
//                {
//                    _layout = _layout + metaComponent.ComponentSetup.ComponentDetails + Environment.NewLine;
//                }
//                else
//                {
//                    var componentList = RecurisiveComponentPlacements(metaComponents.Where(x => x.ParentLayoutComponentSetupId == metaComponent.Id).ToList());
//                    _layout = layout + string.Format(metaComponent.ComponentSetup.ComponentDetails, componentList);
//                }
//            });

//            _layout = _layout + "</head>" + Environment.NewLine +
//"<body>" + Environment.NewLine;

//            var navComponents = layout.LayoutComponentSetups.Where(c => c.ComponentType == "nav").ToList();

//            navComponents.OrderBy(o => o.PlacementOrder).ToList().ForEach(navComponent =>
//            {
//                if (navComponents.Where(w => w.ParentLayoutComponentSetupId == navComponent.Id).Count() == 0)
//                {
//                    _layout = _layout + navComponent.ComponentSetup.ComponentDetails + Environment.NewLine;
//                }
//                else
//                {
//                    var componentList = RecurisiveComponentPlacements(navComponents.Where(x => x.ParentLayoutComponentSetupId == navComponent.Id).ToList());
//                    _layout = layout + string.Format(navComponent.ComponentSetup.ComponentDetails, componentList);
//                }
//            });


//            _layout = _layout + "                 <section id=\"home\">" + Environment.NewLine;
//            _layout = _layout + "                    @RenderBody()";
//            _layout = _layout + "                </section>" + Environment.NewLine;

//            var footerComponents = layout.LayoutComponentSetups.Where(c => c.ComponentType == "footer").ToList();

//            footerComponents.OrderBy(o => o.PlacementOrder).ToList().ForEach(footerComponent =>
//            {
//                if (footerComponents.Where(w => w.ParentLayoutComponentSetupId == footerComponent.Id).Count() == 0)
//                {
//                    _layout = _layout + footerComponent.ComponentSetup.ComponentDetails + Environment.NewLine;
//                }
//                else
//                {
//                    var componentList = RecurisiveComponentPlacements(footerComponents.Where(x => x.ParentLayoutComponentSetupId == footerComponent.Id).ToList());
//                    _layout = layout + string.Format(footerComponent.ComponentSetup.ComponentDetails, componentList);
//                }
//            });

//            _layout = _layout + "</body>" + Environment.NewLine +
//            "</html>" + Environment.NewLine;

//            if (_layout != string.Empty)
//            {

//                string path = _dynamicLayoutPath + @"\" + layout.LayoutName + ".cshtml";

//                if (!Directory.Exists(_dynamicLayoutPath))
//                {
//                    Directory.CreateDirectory(_dynamicLayoutPath);
//                    File.WriteAllText(path, _layout);
//                }
//                else
//                {
//                    File.WriteAllText(path, _layout);
//                }

//            }
//        }
//        public static void GenerateLayout(PageSetup page)
//        {
//            string pageLayout = @"";
//            string layoutPath = string.Format("~/Views/Template/Layout/{0}.cshtml", page.LayoutSetup.LayoutName);
//            pageLayout = pageLayout + "@{" + Environment.NewLine;
//            pageLayout = pageLayout + $"    ViewBag.Title = \"{page.Title}\";" + Environment.NewLine;
//            pageLayout = pageLayout + $"    Layout = \"{layoutPath}\";" + Environment.NewLine;
//            pageLayout = pageLayout + "}" + Environment.NewLine;
//            pageLayout = pageLayout + Environment.NewLine;
//            pageLayout = pageLayout + Environment.NewLine;

//            page.PageComponentSetups.OrderBy(o => o.PlacementOrder).ToList().ForEach(pageComponent =>
//            {
//                if (page.PageComponentSetups.Where(w => w.ParentPageComponentSetupId == pageComponent.Id).Count() == 0)
//                {
//                    pageLayout = pageLayout + pageComponent.ComponentSetup.ComponentDetails + Environment.NewLine;
//                }
//                else
//                {
//                    var componentList = RecurisiveComponentPlacements(page.PageComponentSetups.Where(x => x.ParentPageComponentSetupId == pageComponent.Id).ToList());
//                    pageLayout = pageLayout + string.Format(pageComponent.ComponentSetup.ComponentDetails, componentList);
//                }
//            });

//            if (pageLayout != string.Empty)
//            {

//                string path = _dynamicPagePath + @"\" + page.PageName + ".cshtml";

//                if (!Directory.Exists(_dynamicPagePath))
//                {
//                    Directory.CreateDirectory(_dynamicPagePath);
//                    File.WriteAllText(path, pageLayout);
//                }
//                else
//                {
//                    File.WriteAllText(path, pageLayout);
//                }

//            }
//        }
//        public static List<string> RecurisiveComponentPlacements(List<LayoutComponentSetup> layoutComponentSetups)
//        {
//            List<string> componets = new List<string>();

//            layoutComponentSetups.OrderBy(o => o.PlacementOrder).ToList().ForEach(Component =>
//            {
//                if (layoutComponentSetups.Where(w => w.ParentLayoutComponentSetupId == Component.Id).Count() == 0)
//                {
//                    componets.Add(Component.ComponentSetup.ComponentDetails + Environment.NewLine);
//                }
//                else
//                {
//                    var _childComponets = RecurisiveComponentPlacements(layoutComponentSetups.Where(x => x.ParentLayoutComponentSetupId == Component.Id).ToList());
//                    componets.Add(string.Format(Component.ComponentSetup.ComponentDetails, _childComponets));
//                }
//            });

//            return componets;
//        }
//        public static List<string> RecurisiveComponentPlacements(List<PageComponentSetup> layoutComponentSetups)
//        {
//            List<string> componets = new List<string>();

//            layoutComponentSetups.OrderBy(o => o.PlacementOrder).ToList().ForEach(Component =>
//            {
//                if (layoutComponentSetups.Where(w => w.ParentPageComponentSetupId == Component.Id).Count() == 0)
//                {
//                    componets.Add(Component.ComponentSetup.ComponentDetails + Environment.NewLine);
//                }
//                else
//                {
//                    var _childComponets = RecurisiveComponentPlacements(layoutComponentSetups.Where(x => x.ParentPageComponentSetupId == Component.Id).ToList());
//                    componets.Add(string.Format(Component.ComponentSetup.ComponentDetails, _childComponets));
//                }
//            });

//            return componets;
//        }
//    }
//}