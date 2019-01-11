using FestivalManager.Core.Controllers;
using FestivalManager.Core.Controllers.Contracts;
using FestivalManager.Entities;
using FestivalManager.Entities.Contracts;
using FestivalManager.Entities.Sets;
using System.Collections.Generic;
using System;
using FestivalManager.Entities.Instruments;

namespace FestivalManager.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
	public class SetControllerTests
    {
		[Test]
	    public void TestControllerShouldReturnFailMessage()
	    {
            IStage stage = new Stage();
            ISetController setController = new SetController(stage);

            ISet set =new Short("Set1");
            stage.AddSet(set);

            string actualResult=setController.PerformSets();

            string expectedResult = "1. Set1:\r\n-- Did not perform";

            Assert.That(actualResult, Is.EqualTo(expectedResult));
		}

        [Test]
        public void TestControllerShouldReturnSuccessMessage()
        {
            IStage stage = new Stage();
            ISetController setController = new SetController(stage);

            ISet set = new Short("Set1");
            IPerformer performer = new Performer("Pesho",12);
            performer.AddInstrument(new Guitar());
            set.AddPerformer(performer);

            ISong song = new Song("Song", new TimeSpan(0, 3, 22));
            set.AddSong(song);

            stage.AddSet(set);

            string actualResult = setController.PerformSets();
            string expectedResult = "1. Set1:\r\n-- 1. Song (03:22)\r\n-- Set Successful";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PerformSetSouldDecreaseWear()
        {
            IStage stage = new Stage();
            ISetController setController = new SetController(stage);

            ISet set = new Short("Set1");
            IPerformer performer = new Performer("Pesho", 12);
            IInstrument instrument = new Guitar();
            performer.AddInstrument(instrument);
            set.AddPerformer(performer);

            ISong song = new Song("Song", new TimeSpan(0, 3, 22));
            set.AddSong(song);

            stage.AddSet(set);

            double instrumentWearBeforePerformance = instrument.Wear;

            setController.PerformSets();

            double instrumentWearAfterPerformance = instrument.Wear;

            Assert.That(instrumentWearBeforePerformance, Is.Not.EqualTo(instrumentWearAfterPerformance));
        }
    }
}