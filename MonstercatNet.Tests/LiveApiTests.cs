using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class LiveApiTests : TestBase
    {
        internal Playlist Playlist { get; private set; }

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
            Assert.IsTrue(filters.Genres.Length > 0);
            Assert.IsTrue(filters.Tags.Length > 0);
            Assert.IsTrue(filters.Types.Length > 0);
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

        [Test, Order(5)]
        public async Task Test_GetReleases()
        {
            var releases = await Api.GetReleases(new ReleaseBrowseRequest()
            {
                Limit = 1,
                Skip = 0
            });

            Assert.IsNotNull(releases);
            Assert.IsTrue(releases.Results.Length == 1);
            Assert.IsNotNull(releases.Results[0]);
        }

        [Test, Order(6)]
        public async Task Test_GetRelease()
        {
            var release = await Api.GetRelease("MCRLX001-8");

            Assert.IsNotNull(release);
            Assert.IsNotNull(release.Release);
            Assert.IsNotNull(release.Tracks);
            Assert.IsTrue(release.Tracks.Length == 1);
        }

        [Test, Order(7)]
        public async Task Test_GetReleaseCoverAsByteArray()
        {
            var cover = await Api.GetReleaseCoverAsByteArray(new ReleaseCoverRequest()
            {
                ReleaseId = Guid.Parse("466c62cd-cfa8-457d-9dbf-66db101d73a6"),
            });

            Assert.IsNotNull(cover);
            Assert.IsTrue(cover.Length > 0);
        }

        [Test, Order(8)]
        public async Task Test_GetReleaseCoverAsStream()
        {
            var cover = await Api.GetReleaseCoverAsStream(new ReleaseCoverRequest()
            {
                ReleaseId = Guid.Parse("466c62cd-cfa8-457d-9dbf-66db101d73a6"),
            });

            Assert.IsNotNull(cover);

            var result = cover.ToByteArray();
            Assert.IsTrue(result.Length > 0);
        }

        // requires active gold subscription
        [Test, Order(9)]
        public async Task Test_DownloadReleaseAsByteArray()
        {
            var release = await Api.DownloadReleaseAsByteArray(new ReleaseDownloadRequest()
            {
                ReleaseId = Guid.Parse("466c62cd-cfa8-457d-9dbf-66db101d73a6"),
            });

            Assert.IsNotNull(release);
            Assert.IsTrue(release.Length > 0);
        }

        // requires active gold subscription
        [Test, Order(10)]
        public async Task Test_DownloadReleaseAsStream()
        {
            var release = await Api.DownloadReleaseAsStream(new ReleaseDownloadRequest()
            {
                ReleaseId = Guid.Parse("466c62cd-cfa8-457d-9dbf-66db101d73a6"),
            });

            Assert.IsNotNull(release);

            var result = release.ToByteArray();
            Assert.IsTrue(result.Length > 0);
        }

        // requires active gold subscription
        [Test, Order(11)]
        public async Task Test_DownloadTrackAsByteArray()
        {
            var release = await Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.IsNotNull(release);
            Assert.IsTrue(release.Length > 0);
        }

        // requires active gold subscription
        [Test, Order(12)]
        public async Task Test_DownloadTrackAsStream()
        {
            var release = await Api.DownloadTrackAsStream(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.IsNotNull(release);

            var result = release.ToByteArray();
            Assert.IsTrue(result.Length > 0);
        }

        [Test, Order(13)]
        public async Task Test_StreamTrackAsStream()
        {
            var release = await Api.StreamTrackAsStream(new TrackStreamRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.IsNotNull(release);

            var result = release.ToByteArray();
            Assert.IsTrue(result.Length > 0);
        }

        [Test, Order(14)]
        public async Task Test_CreatePlaylist()
        {
            Playlist = await Api.CreatePlaylist(new PlaylistCreateRequest()
            {
                Name = $"MyTestPlaylist",
                Public = false,
                Tracks = new PlaylistCreateTrack[]
                {
                    new PlaylistCreateTrack()
                    {
                        ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                        TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
                    }
                }
            });

            Assert.IsNotNull(Playlist);
        }

        [Test, Order(15)]
        public async Task Test_PlaylistGetTracklist()
        {
            var tracklist = await Api.GetPlaylistTracks(Playlist.Id);

            Assert.IsNotNull(tracklist);
            Assert.IsNotNull(tracklist.Results);
            Assert.IsTrue(tracklist.Results.Length == 1);
            Assert.IsTrue(tracklist.Results[0].Id == Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f"));
        }

        [Test, Order(16)]
        public async Task Test_PlaylistAddTrack()
        {
            await Api.PlaylistAddTrack(new PlaylistAddTrackRequest()
            {
                PlaylistId = Playlist.Id,
                ReleaseId = Guid.Parse("ff361c51-ed8c-49f7-b693-c04a1e01dcca"),
                TrackId = Guid.Parse("95f781ba-2737-41aa-83a1-115e73b879a8")
            });
        }

        [Test, Order(17)]
        public async Task Test_PlaylistDeleteTrack()
        {
            await Api.PlaylistDeleteTrack(Playlist.Id, new PlaylistDeleteTrackRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });
        }

        [Test, Order(18)]
        public async Task Test_GetSelfPlaylists()
        {
            var playlists = await Api.GetSelfPlaylists();

            Assert.IsNotNull(playlists);

            Assert.IsTrue(playlists.Results.Length >= 1);
            Assert.IsNotNull(playlists.Results.FirstOrDefault(p => p.Id == Playlist.Id));
        }

        [Test, Order(19)]
        public async Task Test_GetPlaylist()
        {
            var playlist = await Api.GetPlaylist(Playlist.Id);

            Assert.IsNotNull(playlist);
        }

        [Test, Order(20)]
        public async Task Test_RenamePlaylist()
        {
            var playlist = await Api.RenamePlaylist(Playlist.Id, new PlaylistRenameRequest() { Name = "MyRenameTestPlaylist" });

            Assert.AreEqual("MyRenameTestPlaylist", playlist.Name);
        }

        [Test, Order(21)]
        public async Task Test_MakePlaylistPublic()
        {
            var playlist = await Api.SwitchPlaylistAvailability(Playlist.Id, new PlaylistSwitchAvailabilityRequest() { Public = true });

            Assert.AreEqual(true, playlist.Public);
        }

        [Test, Order(22)]
        public async Task Test_MakePlaylistPrivate()
        {
            var playlist = await Api.SwitchPlaylistAvailability(Playlist.Id, new PlaylistSwitchAvailabilityRequest() { Public = false });

            Assert.AreEqual(false, playlist.Public);
        }

        [Test, Order(23)]
        public async Task Test_DeletePlaylist()
        {
            if (Playlist is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            await Api.DeletePlaylist(Playlist.Id);
        }

        [Test, Order(999)]
        public async Task Test_Logout()
        {
            await Api.Logout();
        }
    }
}
