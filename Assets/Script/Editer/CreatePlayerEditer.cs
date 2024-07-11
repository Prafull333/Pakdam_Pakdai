using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.VisualScripting;

public class CreatePlayerEditer : EditorWindow
{
    [MenuItem("Pakdam Pakdai/Create Player")]
    public static void showWindow()
    {
        GetWindow<CreatePlayerEditer>("Create Player");
    }
    public GameObject modal;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("modal"));

        if (modal == null)
        {
            EditorGUILayout.HelpBox("Please assine object to create NPC,", MessageType.Info);
        }
        else
        {
            if (modal.GetComponent<Animator>().avatar != null)
            {
                EditorGUILayout.BeginVertical("Box");
                CreateButton();
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.HelpBox("Please assine Humaniod Modal,", MessageType.Error);
            }
        }

        obj.ApplyModifiedProperties();
    }

    public void CreateButton()
    {
        if (GUILayout.Button("Create NPC"))
        {
            CreateNPCPlayer();
        }

        if (GUILayout.Button("Create Player"))
        {
            CreatePlayer();
        }
    }

    void CreateNPCPlayer()
    {
        GameObject p = modal; 
        CharacterController cc = p.AddComponent<CharacterController>();
        cc.slopeLimit = 45;
        cc.stepOffset = 0.25f;
        cc.skinWidth = 0.02f;
        cc.minMoveDistance = 0;
        cc.center = new Vector3(0, 0.93f, 0);
        cc.radius = 0.28f;
        cc.height = 1.8f;

        p.AddComponent<BasicRigidBodyPush>();
        p.AddComponent<PlayerInput>();
        p.AddComponent<Player>();

        AiPlayer ai = p.AddComponent<AiPlayer>();
        ai.GroundLayers = LayerMask.GetMask("Ground");
    }

    void CreatePlayer()
    {
        GameObject p = modal;
        CharacterController cc = p.AddComponent<CharacterController>();
        cc.slopeLimit = 45;
        cc.stepOffset = 0.25f;
        cc.skinWidth = 0.02f;
        cc.minMoveDistance = 0;
        cc.center = new Vector3(0, 0.93f, 0);
        cc.radius = 0.28f;
        cc.height = 1.8f;

        p.AddComponent<BasicRigidBodyPush>();
        p.AddComponent<PlayerInput>();
        p.AddComponent<Player>();
        p.AddComponent<StarterAssetsInputs>();

        ThirdPersonController controller = p.AddComponent<ThirdPersonController>();
        controller.GroundLayers = LayerMask.GetMask("Ground");

        GameObject cameraRoot =  Instantiate(new GameObject("Camera Root"));
        cameraRoot.transform.parent = p.transform;
        cameraRoot.transform.localPosition = new Vector3(0, 1.3f, -0.5f);
        
        controller.CinemachineCameraTarget = cameraRoot;
    }
}
