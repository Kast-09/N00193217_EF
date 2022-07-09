using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N00193217.Web.Controllers;
using N00193217.Web.Repositorio;
using Moq;

namespace N00193217.Test.Controllers
{
    public class HomeControllerTest
    {
        [Test]
        public void IndexViewCase01()
        {
            var indexT = new HomeController();
            var view = indexT.Index();

            Assert.IsNotNull(view);
        }

        [Test]
        public void PrivacyViewCase01()
        {
            var privacyT = new HomeController();
            var view = privacyT.Index();

            Assert.IsNotNull(view);
        }
    }
}
