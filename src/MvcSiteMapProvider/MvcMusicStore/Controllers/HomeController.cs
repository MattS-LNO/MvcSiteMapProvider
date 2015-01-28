using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);

            return View(albums);
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        [MvcSiteMapNode(IsVideo = true, Key = "NewPage", ParentKey = "Index", CanonicalUrlSeo = "/new-page",
            Title = "New Video Page", Description = "A description about New Video Page.",
            ContentLocationUrl = "http://html5.learnnowonline.com/T/SRCC/SRC2.MP4", ExpirationDate = "2017-02-01T23:59:59-06:00", FamilyFriendly = true, GalleryLocation = "http://www.learnnowonline.com/courses",
            GalleryTitle = "LearnNowOnline", ImageUrl = "http://ca1.learnnowonline.com/content/images/thumbnails/src2.png", LastModifiedDate = "2014-12-01T11:59:59-06:00", PlayerAllowEmbed = true,
            PlayerLocationUrl = "http://html5.learnnowonline.com/T/SRCC/SRC2.MP4", RequiresSubscription = true, VideoLive = true, VideoRating = 4.2,
            VideoUploader = "Learn Now, Inc.", VideoUploaderUrl = "http://www.learnnowonline.com/company", ViewCount = 123456,
            VideoDuration = 98
            )]
        public ActionResult NewPage()
        {
            return RedirectToAction("Index");
        }

        public ActionResult SiteMap()
        {
            return View();
        }
    }
}
