using HintMachine.Models.GenericConnectors;

namespace HintMachine.Models.Games
{
    public class GuitarHeroIIIConnector : IGameConnector
    {
        private ProcessRamWatcher _ram = null;

        public GuitarHeroIIIConnector()
        {
            Name = "Guitar Hero III";
            Description =
                "Guitar Hero III: Legends of Rock is a 2007 rhythm game developed by Neversoft and published by Activision. It is the third main installment, the fourth overall installment in the Guitar Hero series and the first game in the series to be developed by Neversoft after Activision's acquisition of RedOctane and MTV Games' purchase of Harmonix, the previous development studio for the series. The game was released worldwide for the PlayStation 2, PlayStation 3, Xbox 360, and Wii in October 2007. Aspyr published the Microsoft Windows and Mac OS X versions of the game, releasing them later in 2007.";
            Platform = "PC";
            SupportedVersions.Add("BetterGH3");
            Author = "Neversoft";
            Quests.Add(_scoreQuest);
        }
        private readonly HintQuestCumulative _scoreQuest = new HintQuestCumulative
        {
            Name = "Score",
            GoalValue = 500000,
        };
        protected override bool Connect()
        {
            _ram = new ProcessRamWatcher("GH3");
            return _ram.TryConnect();
        }

        public override void Disconnect()
        {
            _ram = null;
        }

        protected override bool Poll()
        {
            long scoreAddress = 0xA12B00;
            _scoreQuest.UpdateValue(_ram.ReadUint32(scoreAddress));

            return true;
        }
    }
}