using System;

namespace Common
{
    public interface IDetailViewModel
    {
        void SetCurrentItem(int id, Action<int> onItemUpdated);
    }

    public class DetailViewModel : IDetailViewModel
    {
        private readonly IDataStorage<Person> _storage;

        private Action<int>? onItemUpdated { get; set; }

        public virtual Person? Item { get; set; }

        public DetailViewModel(IDataStorage<Person> storage) 
        {
            _storage = storage;
        }

        void IDetailViewModel.SetCurrentItem(int id, Action<int> onItemUpdated)
        {
            Item = _storage.Find(id);
            this.onItemUpdated = onItemUpdated;
        }

        public void Update()
        {
            _storage.Update(Item!);
            onItemUpdated?.Invoke(Item!.Id);
        }

        public bool CanUpdate() => Item != null;
    }
}
