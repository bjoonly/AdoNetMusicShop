using System;
using System.Data.Entity;

namespace DAL
{
    internal class Initializer : DropCreateDatabaseIfModelChanges<MusicShopDbContext>
    {
        protected override void Seed(MusicShopDbContext context)
        {
            base.Seed(context);

            Account admin = new Account() { Login = "admin", Password = "0ffe1abd1a08215353c233d6e009613e95eec4253832a761af28ff37ac5a150c", IsClient = false };
            Account ac1 = new Account() { Login = "dr4m4", Password = "0ffe1abd1a08215353c233d6e009613e95eec4253832a761af28ff37ac5a150c", IsClient = true };
            Account ac2 = new Account() { Login = "petro", Password = "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb", IsClient = true };

            context.Accounts.Add(admin);
            context.Accounts.Add(ac1);
            context.Accounts.Add(ac2);
            context.SaveChanges();


            Client dr4m4client = new Client() { Name = "Roman", Surname = "Smirnov", Email = "dr4m4@gmail.com", AccountId = ac1.Id, Phone = "+38030303003" };
            Client petroclient = new Client() { Name = "Petro", Surname = "Ivanov", Email = "petro@gmail.com", AccountId = ac2.Id, Phone = "+3808383833" };

            context.Clients.Add(dr4m4client);
            context.Clients.Add(petroclient);
            context.SaveChanges();

            Genre pop = new Genre() { Name = "Pop" };
            Genre poppunk = new Genre() { Name = "Pop punk" };
            Genre alrock = new Genre() { Name = "Alternative rock" };
            Genre hipHop = new Genre() { Name = "Hip-Hop" };

            Genre emorap = new Genre() { Name = "Emo rap" };
            Genre emotrap = new Genre() { Name = "Emo trap" };
            Genre lofi = new Genre() { Name = "Lo-fi" };
            Genre rB = new Genre() { Name = "R&B" };

            Genre tripHop = new Genre() { Name = "Trip hop" };


            context.Genres.Add(pop);
            context.Genres.Add(poppunk);
            context.Genres.Add(alrock);
            context.Genres.Add(hipHop);

            context.Genres.Add(emorap);
            context.Genres.Add(emotrap);
            context.Genres.Add(lofi);
            context.Genres.Add(rB);

            context.Genres.Add(tripHop);

            context.SaveChanges();

            Artist yungBlud = new Artist() { Name = "YUNGBLUD" };
            Artist lilpeep = new Artist() { Name = "Lil Peep" };
            Artist oliverTree = new Artist() { Name = "Oliver Tree" };
            Artist joji = new Artist() { Name = "Joji" };

            Artist girlInRed = new Artist() { Name = "Girl in Red" };
            Artist juiceWRLD = new Artist() { Name = "Juice WRLD" };
            Artist xXXTentacion = new Artist() { Name = "XXXTentacion" };
            Artist billieEilish = new Artist() { Name = "Billie Eilish" };


            context.Artists.Add(yungBlud);
            context.Artists.Add(lilpeep);
            context.Artists.Add(oliverTree);
            context.Artists.Add(joji);

            context.Artists.Add(girlInRed);
            context.Artists.Add(juiceWRLD);
            context.Artists.Add(xXXTentacion);
            context.Artists.Add(billieEilish);

            context.SaveChanges();

            Publisher p1 = new Publisher() { Name = "Universal Music Group" };
            Publisher p2 = new Publisher() { Name = "Locomotion Recordings" };
            Publisher p3 = new Publisher() { Name = "AWAL" };
            Publisher p4 = new Publisher() { Name = "Columbia Records" };
            Publisher p5 = new Publisher() { Name = "Atlantic Records" };
            Publisher p6 = new Publisher() { Name = "88rising Music" };


            context.Publishers.Add(p1);
            context.Publishers.Add(p2);
            context.Publishers.Add(p3);
            context.Publishers.Add(p4);
            context.Publishers.Add(p5);
            context.Publishers.Add(p6);

            context.SaveChanges();

            Deal dealHipHop = new Deal() { Name = "Hip-Hop", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Discount = 10 };
            context.Deals.Add(dealHipHop);


            Record weird = new Record()
            {
                ArtistId = yungBlud.Id,
                PublisherId = p1.Id,
                Name = "a weird! af halloween",
                SellingPrice = 220,
                CountSongs = 6,
                CostPrice = 180,
                DateOfPublishing = new System.DateTime(2020, 10, 30),
                GenreId = alrock.Id
            };
            Record underrated = new Record()
            {
                ArtistId = yungBlud.Id,
                PublisherId = p2.Id,
                Name = "the underrated youth",
                SellingPrice = 250,
                CountSongs = 6,
                CostPrice = 220,
                DateOfPublishing = new System.DateTime(2019, 10, 18),
                GenreId = alrock.Id
            };
            Record century = new Record()
            {
                ArtistId = yungBlud.Id,
                PublisherId = p2.Id,
                Name = "21st Century Liability",
                CostPrice = 420,
                SellingPrice = 500,
                CountSongs = 12,
                DateOfPublishing = new System.DateTime(2018, 7, 6),
                GenreId = alrock.Id
            };
            //-----------------------------------------------
            Record pt1 = new Record()
            {
                ArtistId = lilpeep.Id,
                PublisherId = p3.Id,
                Name = "Come Over When You're Sober, Pt. 1",
                SellingPrice = 100,
                CountSongs = 2,
                CostPrice = 80,
                DateOfPublishing = new System.DateTime(2017, 9, 15),
                GenreId = emorap.Id
            };
            Record pt2 = new Record()
            {
                ArtistId = lilpeep.Id,
                PublisherId = p4.Id,
                Name = "Come Over When You're Sober, Pt. 2",
                SellingPrice = 550,
                CountSongs = 11,
                CostPrice = 450,
                Deal=dealHipHop,
                DateOfPublishing = new System.DateTime(2018, 11, 9),
                GenreId = hipHop.Id
            };
            Record crybaby = new Record()
            {
                ArtistId = lilpeep.Id,
                PublisherId = p1.Id,
                Name = "Crybaby",
                SellingPrice = 550,
                CountSongs = 11,
                CostPrice = 480,
                Deal = dealHipHop,
                DateOfPublishing = new System.DateTime(2016, 6, 10),
                GenreId = hipHop.Id
            };
            Record ee = new Record()
            {
                ArtistId = lilpeep.Id,
                PublisherId = p4.Id,
                Name = "Everybody’s everything",
                SellingPrice = 670,
                CountSongs = 19,
                CostPrice = 600,
                DateOfPublishing = new System.DateTime(2018, 7, 6),
                GenreId = emorap.Id
            };
            Record gas= new Record()
            {
                ArtistId = lilpeep.Id,
                PublisherId = p2.Id,
                Name = "Goth Angel Sinner",
                SellingPrice = 100,
                CountSongs = 3,
                CostPrice = 90,
                DateOfPublishing = new System.DateTime(2019, 10, 31),
                GenreId = emorap.Id
            };
            Record hellboy = new Record()
            {
                ArtistId = lilpeep.Id,
                PublisherId = p2.Id,
                Name = "Hellboy",
                SellingPrice = 650,
                CountSongs = 16,
                CostPrice = 600,
                Deal = dealHipHop,
                DateOfPublishing = new System.DateTime(2015, 9, 25),
                GenreId = hipHop.Id
            };
            //-------------------------------------
            Record doyoufeelme = new Record()
            {
                ArtistId = oliverTree.Id,
                PublisherId = p5.Id,
                Name = "do you feel me?",
                SellingPrice = 270,
                CountSongs = 6,
                CostPrice = 220,
                DateOfPublishing = new System.DateTime(2019, 8, 6),
                GenreId = alrock.Id
            };
            Record alienboy = new Record()
            {
                ArtistId = oliverTree.Id,
                PublisherId = p5.Id,
                Name = "alien boy ep",
                SellingPrice = 210,
                CountSongs = 6,
                CostPrice = 180,
                DateOfPublishing = new System.DateTime(2019, 5, 17),
                GenreId = alrock.Id
            };
            Record ugly = new Record()
            {
                ArtistId = oliverTree.Id,
                PublisherId = p5.Id,
                Name = "Ugly Is Beautiful",
                SellingPrice = 620,
                CountSongs = 14,
                CostPrice = 580,
                DateOfPublishing = new System.DateTime(2020,7, 17),
                GenreId = alrock.Id
            };
            //-----------------------------------------
            Record nectar = new Record()
            {
                ArtistId = joji.Id,
                PublisherId = p6.Id,
                Name = "Nectar",
                SellingPrice = 850,
                CountSongs = 18,
                CostPrice = 800,
                DateOfPublishing = new System.DateTime(2020, 9, 25),
                GenreId = lofi.Id
            };
            Record ballads = new Record()
            {
                ArtistId = joji.Id,
                PublisherId = p6.Id,
                Name = "Ballads 1",
                SellingPrice = 650,
                CountSongs = 12,
                CostPrice = 600,
                DateOfPublishing = new System.DateTime(2018, 10, 26),
                GenreId = lofi.Id
            };
            Record inTongues = new Record()
            {
                ArtistId = joji.Id,
                PublisherId = p6.Id,
                Name = "In Tongues",
                SellingPrice = 250,
                CountSongs = 6,
                CostPrice = 220,
                DateOfPublishing = new System.DateTime(2017, 11, 3),
                GenreId = rB.Id
            };
            Record pinkSeason = new Record()
            {
                ArtistId = joji.Id,
                PublisherId = p6.Id,
                Name = "Pink Season",
                SellingPrice = 900,
                CountSongs = 35,
                CostPrice = 780,
                DateOfPublishing = new System.DateTime(2017, 1, 4),
                GenreId = rB.Id            };
            Record pinkSeasonTheProphecy = new Record()
            {
                ArtistId = joji.Id,
                PublisherId = p6.Id,
                Name = "Pink Season: The Prophecy",
                SellingPrice = 200,
                CountSongs = 5,
                CostPrice = 180,
                DateOfPublishing = new System.DateTime(2017, 5, 24),
                GenreId = rB.Id
            };
            //------------------------------------------------



            context.Records.Add(weird);
            context.Records.Add(underrated);
            context.Records.Add(century);
            context.Records.Add(pt1);
            context.Records.Add(pt2);
            context.Records.Add(crybaby);
            context.Records.Add(ee);
            context.Records.Add(gas);
            context.Records.Add(hellboy);

            context.Records.Add(doyoufeelme);
            context.Records.Add(alienboy);
            context.Records.Add(ugly);
            



            context.Records.Add(nectar);
            context.Records.Add(ballads);
            context.Records.Add(inTongues);
            context.Records.Add(pinkSeason);
            context.Records.Add(pinkSeasonTheProphecy);

            context.SaveChanges();
            //--------------------------------------

           

        }
    }
}