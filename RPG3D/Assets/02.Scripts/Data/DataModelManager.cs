using RPG.Singletons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Data
{
    public enum DataCategory
    {
        None,
        Inventory,
    }

    //Repository pattern
    public class DataModelManager : SingletonBase<DataModelManager>
    {
        private Dictionary<Type, IDataModel> _dataModelsByType;
        private Dictionary<DataCategory, IDataModel> _dataModelsByCategory;

        public bool TryGet<T>(out T dataModel) where T : IDataModel
        {
            if (_dataModelsByType.TryGetValue(typeof(T), out IDataModel result))
            {
                dataModel = (T)result;
                return true;
            }

            dataModel = default(T);
            return false;
        }

        public bool TryGet(DataCategory category, out IDataModel dataModel)
        {
            return _dataModelsByCategory.TryGetValue(category, out dataModel);
        }

        public void Register<T>(DataCategory category)
            where T : IDataModel
        {
            if (_dataModelsByType.ContainsKey(typeof(T)))
                throw new Exception($"[DataModelManager] : Failed to register.  {typeof(T)} is already exist. ");

            T dataModel = Load<T>();  
            //원래 생성자 로드했는데 파일로 저장하면 걍 파일에서 갖고오면됨 -> Register 함수에서 생성자로드필요X

            _dataModelsByType.Add(typeof(T), dataModel);
            _dataModelsByCategory.Add(category, dataModel);
        }

        public T Load<T>()
            where T : IDataModel
        {
            string dataPath = $"{Application.persistentDataPath}/{typeof(T).Name}.json";

            T data;
            if (System.IO.File.Exists(dataPath))
            {
                data = JsonUtility.FromJson<T>(System.IO.File.ReadAllText(dataPath));  //data 읽어와서 객체로 바꿔줌
            }
            else
            {
                data = (T)Activator.CreateInstance<T>().ResetWithDefaults();
                System.IO.File.WriteAllText(dataPath, JsonUtility.ToJson(data));
            }

            return data;
        }

        public void Save<T>()
        {
            string dataPath = $"{Application.persistentDataPath}/{typeof(T).Name}.json";
            System.IO.File.WriteAllText(dataPath, JsonUtility.ToJson(_dataModelsByType[typeof(T)]));
        }

        protected override void Init()
        {
            base.Init();
            _dataModelsByType = new Dictionary<Type, IDataModel>();
            _dataModelsByCategory = new Dictionary<DataCategory, IDataModel>();

            Register<InventoryData>(DataCategory.Inventory);

            /*_dataModels = new Dictionary<DataCategory, IDataModel>
            //{
            //    {DataCategory.Inventory, new InventoryData(36) },
            //};
            _dataModels[DataCategory.Inventory].id = (int)DataCategory.Inventory;*/
        }
    }
}
