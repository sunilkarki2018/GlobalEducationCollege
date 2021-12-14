using GlobalCollege.API.Utility;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Entity.ViewComponent;
using GlobalCollege.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GlobalCollege.API.Controllers.ViewComponent
{
    [Authorize]
    [ExceptionHandler]
    public class ViewComponenInformationController : ApiController
    {
        private readonly IViewComponentRepository _viewComponentReposiotry;
        public ViewComponenInformationController(IViewComponentRepository viewComponentReposiotry)
        {
            _viewComponentReposiotry = viewComponentReposiotry;
        }

        // GET: ViewComponenInformation
        [Authorize]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/viewcomponeninformation/GetViewComponentInformation")]
        public async Task<dynamic> GetViewComponentInformation(string ViewComponentName, string ProcedureName, string Root, Guid? Id, string Parameters)
        {

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            if (Parameters != null)
            {
                Parameters.Split(';').ToList().ForEach(p =>
                {
                    string Key = p.Split('=').First();
                    string Value = p.Split('=').Last();

                    keyValuePairs.Add(Key, Value);

                });
            }

            try
            {
                if (Root == "BannerViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<BannerViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "BlogViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<BlogViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "ProgramViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<ProgramViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "CourseViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<CourseViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "EventsViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<EventsViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "FactsViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<FactsViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "FooterViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<FooterViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }
                if (Root == "MenuViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<MenuViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "MessageViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<MessageViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "NewsViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<NewsViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "TeamViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<TeamViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "TestimonialsViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<TestimonialsViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "WhyusViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<WhyusViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "WidgetsViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<WidgetsViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "GalleryViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<GalleryViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "FAQViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<FAQViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                if (Root == "LifeatGCIViewComponentModel")
                {
                    var viewcomponetInformation = await _viewComponentReposiotry.GetViewComponentInformation<LifeatGCIViewComponentModel>(ViewComponentName, ProcedureName, Root, Id, keyValuePairs);
                    return viewcomponetInformation;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/viewcomponeninformation/GetDetailsViewComponentInformation")]
        public async Task<FrontendPageInformation> GetDetailsViewComponentInformation(string TableName, Guid Id)
        {
            try
            {
                var viewcomponetInformation = await _viewComponentReposiotry.GetDetailViewComponentInformation(TableName, Id);
                return viewcomponetInformation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}