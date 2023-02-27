using UniRx;

namespace Data
{
    public class GameData
    {
        public ReactiveProperty<int> MajorStage = new ReactiveProperty<int>();
        public ReactiveProperty<int> MinorStage = new ReactiveProperty<int>();
        public ReactiveProperty<int> energy = new ReactiveProperty<int>();
    }
}