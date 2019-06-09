//
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
////UGUI的扩展，自定义自己的UI控件
//namespace UnityEditor.UI
//{
//    public static class UGUIExtend
//    {
//        private static DefaultControls.Resources s_StandardResources;
//
//        private static DefaultControls.Resources GetStandardResources()
//        {
//            if ((Object) UGUIExtend.s_StandardResources.standard == (Object) null)
//            {
//                UGUIExtend.s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
//                UGUIExtend.s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
//                UGUIExtend.s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
//                UGUIExtend.s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
//                UGUIExtend.s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");
//                UGUIExtend.s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/DropdownArrow.psd");
//                UGUIExtend.s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
//            }
//            return UGUIExtend.s_StandardResources;
//        }
//        public static void PlaceUIElementRoot(GameObject element, MenuCommand menuCommand)
//        {
//            GameObject parent = menuCommand.context as GameObject;
//            if ((Object) parent == (Object) null || (Object) parent.GetComponentInParent<Canvas>() == (Object) null)
//                parent = GetOrCreateCanvasGameObject();
//            string uniqueNameForSibling = GameObjectUtility.GetUniqueNameForSibling(parent.transform, element.name);
//            element.name = uniqueNameForSibling;
//            Undo.RegisterCreatedObjectUndo((Object) element, "Create " + element.name);
//            Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);
//            GameObjectUtility.SetParentAndAlign(element, parent);
//            if ((Object) parent != menuCommand.context)
//                SetPositionVisibleinSceneView(parent.GetComponent<RectTransform>(), element.GetComponent<RectTransform>());
//            Selection.activeGameObject = element;
//        }
//        
//        public static GameObject GetOrCreateCanvasGameObject()
//        {
//            GameObject activeGameObject = Selection.activeGameObject;
//            Canvas canvas = !((Object) activeGameObject != (Object) null) ? (Canvas) null : activeGameObject.GetComponentInParent<Canvas>();
//            if ((Object) canvas != (Object) null && canvas.gameObject.activeInHierarchy)
//                return canvas.gameObject;
//            Canvas objectOfType = Object.FindObjectOfType(typeof (Canvas)) as Canvas;
//            if ((Object) objectOfType != (Object) null && objectOfType.gameObject.activeInHierarchy)
//                return objectOfType.gameObject;
//            return CreateNewUI();
//        }
//        
//        private static void CreateEventSystem(bool select, GameObject parent)
//        {
//            EventSystem eventSystem = Object.FindObjectOfType<EventSystem>();
//            if ((Object) eventSystem == (Object) null)
//            {
//                GameObject child = new GameObject("EventSystem");
//                GameObjectUtility.SetParentAndAlign(child, parent);
//                eventSystem = child.AddComponent<EventSystem>();
//                child.AddComponent<StandaloneInputModule>();
//                Undo.RegisterCreatedObjectUndo((Object) child, "Create " + child.name);
//            }
//            if (!select || !((Object) eventSystem != (Object) null))
//                return;
//            Selection.activeGameObject = eventSystem.gameObject;
//        }
//        
//        public static GameObject CreateNewUI()
//        {
//            GameObject gameObject = new GameObject("Canvas");
//            gameObject.layer = LayerMask.NameToLayer("UI");
//            gameObject.AddComponent<Canvas>().renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
//            gameObject.AddComponent<CanvasScaler>();
//            gameObject.AddComponent<GraphicRaycaster>();
//            Undo.RegisterCreatedObjectUndo((Object) gameObject, "Create " + gameObject.name);
//            CreateEventSystem(false,null);
//            return gameObject;
//        }
//        
//        private static void SetPositionVisibleinSceneView(
//      RectTransform canvasRTransform,
//      RectTransform itemTransform)
//    {
//      SceneView sceneView = SceneView.lastActiveSceneView;
//      if ((Object) sceneView == (Object) null && SceneView.sceneViews.Count > 0)
//        sceneView = SceneView.sceneViews[0] as SceneView;
//      if ((Object) sceneView == (Object) null || (Object) sceneView.camera == (Object) null)
//        return;
//      Camera camera = sceneView.camera;
//      Vector3 zero = Vector3.zero;
//      Vector2 localPoint;
//      if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2((float) (camera.pixelWidth / 2), (float) (camera.pixelHeight / 2)), camera, out localPoint))
//      {
//        localPoint.x += canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
//        localPoint.y += canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;
//        localPoint.x = Mathf.Clamp(localPoint.x, 0.0f, canvasRTransform.sizeDelta.x);
//        localPoint.y = Mathf.Clamp(localPoint.y, 0.0f, canvasRTransform.sizeDelta.y);
//        zero.x = localPoint.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
//        zero.y = localPoint.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;
//        Vector3 vector3_1;
//        vector3_1.x = (float) ((double) canvasRTransform.sizeDelta.x * (0.0 - (double) canvasRTransform.pivot.x) + (double) itemTransform.sizeDelta.x * (double) itemTransform.pivot.x);
//        vector3_1.y = (float) ((double) canvasRTransform.sizeDelta.y * (0.0 - (double) canvasRTransform.pivot.y) + (double) itemTransform.sizeDelta.y * (double) itemTransform.pivot.y);
//        Vector3 vector3_2;
//        vector3_2.x = (float) ((double) canvasRTransform.sizeDelta.x * (1.0 - (double) canvasRTransform.pivot.x) - (double) itemTransform.sizeDelta.x * (double) itemTransform.pivot.x);
//        vector3_2.y = (float) ((double) canvasRTransform.sizeDelta.y * (1.0 - (double) canvasRTransform.pivot.y) - (double) itemTransform.sizeDelta.y * (double) itemTransform.pivot.y);
//        zero.x = Mathf.Clamp(zero.x, vector3_1.x, vector3_2.x);
//        zero.y = Mathf.Clamp(zero.y, vector3_1.y, vector3_2.y);
//      }
//      itemTransform.anchoredPosition = (Vector2) zero;
//      itemTransform.localRotation = Quaternion.identity;
//      itemTransform.localScale = Vector3.one;
//    }
//    }
//}