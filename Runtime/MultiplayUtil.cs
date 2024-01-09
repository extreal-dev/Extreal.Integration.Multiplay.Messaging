using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Extreal.Integration.Multiplay.Messaging
{
    public static class MultiplayUtil
    {
        public static int GetGameObjectHash(GameObject target)
        {
            var id = GetHierarchyPath(target);
            return id.GetHashCode();
        }

        public static string GetHierarchyPath(GameObject target)
        {
            var path = string.Empty;
            var current = target.transform;

            while (current != null)
            {
                var index = current.GetSiblingIndex();
                path = "/" + current.name + index + path;
                current = current.parent;
            }
            var belongScene = GetBelongScene(target);
            return "/" + belongScene.name + path;
        }

        public static Scene GetBelongScene(GameObject target)
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                var roots = scene.GetRootGameObjects();
                if (roots.Contains(target.transform.root.gameObject))
                {
                    return scene;
                }
            }
            return default;
        }
    }
}
