using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class NetworkInstantiator : MonoBehaviourPun
    {
        public T InstantiateObject<T>(string prefabPath, Transform parent) where T : Component
        {
            GameObject inst = PhotonNetwork.Instantiate(prefabPath, parent.position, parent.rotation);
            photonView.RPC(nameof(SetParentToPrefab), RpcTarget.AllBuffered,
                inst.GetComponent<PhotonView>().ViewID,
                parent.GetComponent<PhotonView>().ViewID);
            return inst.GetComponent<T>();
        }

        public T InstantiateObject<T>(string prefabPath, Vector3 position, Transform parent) where T : Component
        {
            T inst = InstantiateObject<T>(prefabPath, parent);
            photonView.RPC(nameof(SetPosition), RpcTarget.AllBuffered,
                inst.GetComponent<PhotonView>().ViewID, position);
            return inst.GetComponent<T>();
        }

        [PunRPC]
        private void SetPosition(int objId, Vector3 pos)
        {
            GameObject obj = PhotonView.Find(objId).gameObject;
            obj.transform.position = pos;
        }

        [PunRPC]
        private void SetParentToPrefab(int prefabId, int parentId)
        {
            Transform prefab = PhotonView.Find(prefabId).transform;
            Transform parent = PhotonView.Find(parentId).transform;
            prefab.transform.SetParent(parent);
        }
    }
}