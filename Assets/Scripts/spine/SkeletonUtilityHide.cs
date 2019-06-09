

using UnityEngine;
using System.Collections.Generic;
using Spine;
using System.Reflection;
using Spine.Unity;

//换装类
public class SkeletonUtilityHide : MonoBehaviour
{

    SkeletonUtility skeletonUtility;
    Skeleton skeleton;
    SkeletonRenderer skeletonRenderer;
    Skin activeSkin;

    bool isPrefab;

    Dictionary<Slot, List<Attachment>> attachmentTable = new Dictionary<Slot, List<Attachment>>();

    public void Init()
    {
        skeletonUtility = GetComponent<SkeletonUtility>();
        skeletonRenderer = skeletonUtility.GetComponent<SkeletonRenderer>();
        skeleton = skeletonRenderer.skeleton;
        Skin defaultSkin = skeleton.Data.DefaultSkin;
        Skin skin = skeleton.Skin ?? defaultSkin;
        bool notDefaultSkin = skin != defaultSkin;

        attachmentTable.Clear();
        //Debug.Log(skeleton.Slots.Count);
        for (int i = skeleton.Slots.Count - 1; i >= 0; i--)
        {
            var attachments = new List<Attachment>();
            attachmentTable.Add(skeleton.Slots.Items[i], attachments);
            skin.FindAttachmentsForSlot(i, attachments); // Add skin attachments.
            if (notDefaultSkin) defaultSkin.FindAttachmentsForSlot(i, attachments); // Add default skin attachments.
        }

        activeSkin = skeleton.Skin;

    }
    void Start()
    {

        //hide("yan");
        //show("yan");
    }

    public void setDir(bool dir)
    {

        skeleton.FlipX = dir;
    }



    void OnEnable()
    {
        skeletonUtility = GetComponent<SkeletonUtility>();
        skeletonRenderer = skeletonUtility.GetComponent<SkeletonRenderer>();
        skeleton = skeletonRenderer.Skeleton;

        if (skeleton == null)
        {
            skeletonRenderer.Initialize(false);
            skeletonRenderer.LateUpdate();
            skeleton = skeletonRenderer.skeleton;
        }
    }

    public void show(string name)
    {

        bool requireRepaint = false;
        // Debug.Log(name);
        // Debug.Log(attachmentTable.Count);
        foreach (KeyValuePair<Slot, List<Attachment>> pair in attachmentTable)
        {
            // Debug.Log("foreach");
            Slot slot = pair.Key;
            foreach (var attachment in pair.Value)
            {
                //  GUI.contentColor = slot.Attachment == attachment ? Color.white : Color.grey;
                //EditorGUI.indentLevel = baseIndent + 2;
                //  var icon = Icons.GetAttachmentIcon(attachment);
                //bool isAttached = (attachment == slot.Attachment);
                //bool swap = false;// EditorGUILayout.ToggleLeft(new GUIContent(attachment.Name, icon), attachment == slot.Attachment);

                if (name == attachment.Name)
                {
                    slot.Attachment = attachment;
                    requireRepaint = true;
                    // Debug.Log("requireRepaint true");
                }


                //if (isAttached != swap)
                //{
                //    slot.Attachment = isAttached ? null : attachment;
                //    requireRepaint = true;
                //}
                //else
                //{

                //}
                //GUI.contentColor = Color.white;
            }
        }

        if (requireRepaint)
        {
            skeletonRenderer.LateUpdate();
            // SceneView.RepaintAll();
        }


    }

    public void hide(string name)
    {

        bool requireRepaint = false;

        foreach (KeyValuePair<Slot, List<Attachment>> pair in attachmentTable)
        {
            Slot slot = pair.Key;
            foreach (var attachment in pair.Value)
            {


                if (name == attachment.Name)
                {
                  //  Debug.Log(name + " attachment隐藏成功");
                    slot.Attachment = null;
                    requireRepaint = true;
                }

            }
        }
        if (requireRepaint)
        {
            skeletonRenderer.LateUpdate();
            // SceneView.RepaintAll();
        }
    }
    //	if (!skeletonRenderer.valid) return;

    //	UpdateAttachments();
    //	isPrefab |= PrefabUtility.GetPrefabType(this.target) == PrefabType.Prefab;
    //}

    //		public override void OnInspectorGUI () {
    //			bool requireRepaint = false;
    //			if (skeletonRenderer.skeleton != skeleton || activeSkin != skeleton.Skin) {
    //				UpdateAttachments();
    //			}

    //			if (isPrefab) {
    //				GUILayout.Label(new GUIContent("Cannot edit Prefabs", Icons.warning));
    //				return;
    //			}

    //			if (!skeletonRenderer.valid) {
    //				GUILayout.Label(new GUIContent("Spine Component invalid. Check Skeleton Data Asset.", Icons.warning));
    //				return;	
    //			}

    //			skeletonUtility.boneRoot = (Transform)EditorGUILayout.ObjectField("Bone Root", skeletonUtility.boneRoot, typeof(Transform), true);

    //			using (new EditorGUI.DisabledGroupScope(skeletonUtility.boneRoot != null)) {
    //				if (SpineInspectorUtility.LargeCenteredButton(SpawnHierarchyButtonLabel))
    //					SpawnHierarchyContextMenu();
    //			}

    //			using (new SpineInspectorUtility.BoxScope()) {
    //				debugSkeleton = EditorGUILayout.Foldout(debugSkeleton, "Debug Skeleton");

    //				if (debugSkeleton) {
    //					EditorGUI.BeginChangeCheck();
    //					skeleton.FlipX = EditorGUILayout.ToggleLeft("skeleton.FlipX", skeleton.FlipX);
    //					skeleton.FlipY = EditorGUILayout.ToggleLeft("skeleton.FlipY", skeleton.FlipY);
    //					requireRepaint |= EditorGUI.EndChangeCheck();

    ////					foreach (var t in skeleton.IkConstraints)
    ////						EditorGUILayout.LabelField(t.Data.Name + " " + t.Mix + " " + t.Target.Data.Name);

    //					showSlots.target = EditorGUILayout.Foldout(showSlots.target, SlotsRootLabel);
    //					if (showSlots.faded > 0) {
    //						using (new EditorGUILayout.FadeGroupScope(showSlots.faded)) {
    //							int baseIndent = EditorGUI.indentLevel;
    //							foreach (KeyValuePair<Slot, List<Attachment>> pair in attachmentTable) {
    //								Slot slot = pair.Key;

    //								using (new EditorGUILayout.HorizontalScope()) {
    //									EditorGUI.indentLevel = baseIndent + 1;
    //									EditorGUILayout.LabelField(new GUIContent(slot.Data.Name, Icons.slot), GUILayout.ExpandWidth(false));
    //									EditorGUI.BeginChangeCheck();
    //									Color c = EditorGUILayout.ColorField(new Color(slot.R, slot.G, slot.B, slot.A), GUILayout.Width(60));
    //									if (EditorGUI.EndChangeCheck()) {
    //										slot.SetColor(c);
    //										requireRepaint = true;
    //									}
    //								}

    //								foreach (var attachment in pair.Value) {
    //									GUI.contentColor = slot.Attachment == attachment ? Color.white : Color.grey;
    //									EditorGUI.indentLevel = baseIndent + 2;
    //									var icon = Icons.GetAttachmentIcon(attachment);
    //									bool isAttached = (attachment == slot.Attachment);
    //									bool swap = EditorGUILayout.ToggleLeft(new GUIContent(attachment.Name, icon), attachment == slot.Attachment);
    //									if (isAttached != swap) {
    //										slot.Attachment = isAttached ? null : attachment;
    //										requireRepaint = true;
    //									}
    //									GUI.contentColor = Color.white;
    //								}
    //							}
    //						}
    //					}


    //				}

    //				if (showSlots.isAnimating)
    //					Repaint();
    //			}

    //			if (requireRepaint) {
    //				skeletonRenderer.LateUpdate();
    //				SceneView.RepaintAll();
    //			}
    //		}




    //GUIContent SpawnHierarchyButtonLabel = new GUIContent("Spawn Hierarchy", Icons.skeleton);
    //GUIContent SlotsRootLabel = new GUIContent("Slots", Icons.slotRoot);
    //static AnimBool showSlots = new AnimBool(false);
    //static bool debugSkeleton = false;




    //		void SpawnHierarchyButton (string label, string tooltip, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca, params GUILayoutOption[] options) {
    //			GUIContent content = new GUIContent(label, tooltip);
    //			if (GUILayout.Button(content, options)) {
    //				if (skeletonUtility.skeletonRenderer == null)
    //					skeletonUtility.skeletonRenderer = skeletonUtility.GetComponent<SkeletonRenderer>();
    //
    //				if (skeletonUtility.boneRoot != null) {
    //					return;
    //				}
    //
    //				skeletonUtility.SpawnHierarchy(mode, pos, rot, sca);
    //
    //				SkeletonUtilityBone[] boneComps = skeletonUtility.GetComponentsInChildren<SkeletonUtilityBone>();
    //				foreach (SkeletonUtilityBone b in boneComps) 
    //					AttachIcon(b);
    //			}
    //		}

    //void SpawnHierarchyContextMenu () {
    //	GenericMenu menu = new GenericMenu();

    //	menu.AddItem(new GUIContent("Follow"), false, SpawnFollowHierarchy);
    //	menu.AddItem(new GUIContent("Follow (Root Only)"), false, SpawnFollowHierarchyRootOnly);
    //	menu.AddSeparator("");
    //	menu.AddItem(new GUIContent("Override"), false, SpawnOverrideHierarchy);
    //	menu.AddItem(new GUIContent("Override (Root Only)"), false, SpawnOverrideHierarchyRootOnly);

    //	menu.ShowAsContext();
    //}

    //public static void AttachIcon (SkeletonUtilityBone utilityBone) {
    //	Skeleton skeleton = utilityBone.skeletonUtility.skeletonRenderer.skeleton;
    //	Texture2D icon = utilityBone.bone.Data.Length == 0 ? Icons.nullBone : Icons.boneNib;

    //	foreach (IkConstraint c in skeleton.IkConstraints)
    //		if (c.Target == utilityBone.bone) {
    //			icon = Icons.constraintNib;
    //			break;
    //		}

    //	typeof(EditorGUIUtility).InvokeMember("SetIconForObject", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic, null, null, new object[2] {
    //		utilityBone.gameObject,
    //		icon
    //	});
    //}

    //static void AttachIconsToChildren (Transform root) {
    //	if (root != null) {
    //		var utilityBones = root.GetComponentsInChildren<SkeletonUtilityBone>();
    //		foreach (var utilBone in utilityBones)
    //			AttachIcon(utilBone);
    //	}
    //}

    //void SpawnFollowHierarchy () {
    //	Selection.activeGameObject = skeletonUtility.SpawnHierarchy(SkeletonUtilityBone.Mode.Follow, true, true, true);
    //	AttachIconsToChildren(skeletonUtility.boneRoot);
    //}

    //void SpawnFollowHierarchyRootOnly () {
    //	Selection.activeGameObject = skeletonUtility.SpawnRoot(SkeletonUtilityBone.Mode.Follow, true, true, true);
    //	AttachIconsToChildren(skeletonUtility.boneRoot);
    //}

    //void SpawnOverrideHierarchy () {
    //	Selection.activeGameObject = skeletonUtility.SpawnHierarchy(SkeletonUtilityBone.Mode.Override, true, true, true);
    //	AttachIconsToChildren(skeletonUtility.boneRoot);
    //}

    //void SpawnOverrideHierarchyRootOnly () {
    //	Selection.activeGameObject = skeletonUtility.SpawnRoot(SkeletonUtilityBone.Mode.Override, true, true, true);
    //	AttachIconsToChildren(skeletonUtility.boneRoot);
    //}
}


