using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class LiveApiTests : TestBase
    {
        [Test, Order(1)]
        public async Task Test_Login()
        {
            await Api.Login(Credentials);
        }

        [Test, Order(2)]
        public async Task Test_GetSelf()
        {
            var self = await Api.GetSelf();

            Assert.IsNotNull(self);
            Assert.AreEqual(Credentials.Email, self.Email);
            Assert.IsTrue(self.HasGold, "The test account should have an active gold subscription, otherwise some tests are bound to fail.");
        }

        [Test, Order(3)]
        public async Task Test_GetTrackSearchFilters()
        {
            var filters = await Api.GetTrackSearchFilters();

            Assert.IsNotNull(filters);
            Assert.IsNotNull(filters.Genres.Length > 0);
            Assert.IsNotNull(filters.Tags.Length > 0);
            Assert.IsNotNull(filters.Types.Length > 0);
        }

        [Test, Order(4)]
        public async Task Test_SearchTracks()
        {
            var tracks = await Api.SearchTracks(new TrackSearchRequest()
            {
                Limit = 1,
                Skip = 0,
                Creatorfriendly = true,
                Genres = new[] { "Drumstep" },
                ReleaseTypes = new[] { "Album" },
                Tags = new[] { "Uncaged", "Energetic" },
            });

            Assert.IsNotNull(tracks);
            Assert.AreEqual(1, tracks.Results.Length);
            Assert.AreEqual(Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f"), tracks.Results[0].Id);
        }

        [Test, Order(999)]
        public async Task Test_Logout()
        {
            await Api.Logout();
        }
    }
}
