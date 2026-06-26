using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static frost.Core.Main.UI;

namespace frost.Core
{
    [BepInPlugin("frost", "Frost", "0.0.2")]
    public class Main : BaseUnityPlugin
    {

        /*
            Don't mind this shitty ass code. I'm still trying to learn.
        */

        public static bool HidingCode;
        public static bool Config;
        public static bool Mainshit = true;
        public static bool Showmod;

        public void Start()
        {
            GameObject Object = new GameObject("frost");
            Object.AddComponent<UI>();
            DontDestroyOnLoad(Object);
        }

        public void Update()
        {
        }

        public class UI : MonoBehaviour
        {
            public static Rect Rect = new Rect(20, 90, 200, 60);
            public static byte[] BGcol = new byte[] { 0, 0, 0 }; // RGB
            public static byte[] TXTcol = new byte[] { 255, 255, 255 }; // RGB

            public void OnGUI()
            {
                GUI.backgroundColor = new Color32(BGcol[0], BGcol[1], BGcol[2], 255);
                GUI.contentColor = new Color32(TXTcol[0], TXTcol[1], TXTcol[2], 255);
                if (GUILayout.Button($"{(Showmod ? "Close." : "Open.")}"))  {Showmod = !Showmod; }
                if (Showmod)
                {

                    GUI.skin.label.fontSize = 14;
                    GUI.skin.window.fontSize = 24;

                    GUI.matrix = Matrix4x4.Scale(new Vector3(2f, 2f, 1f));

                    if (Mainshit)
                    {
                        Rect = GUILayout.Window(0, Rect, MainUI, "");
                        Rect.width = 200;
                        Rect.height = 170;
                    }
                    if (Config)
                    {
                        Rect = GUILayout.Window(0, Rect, ConfigTab, "");
                    }
                }
            }

            public static string RoomJoin;

            public static void MainUI(int i)
            {
                GUI.backgroundColor = new Color32(BGcol[0], BGcol[1], BGcol[2], 255);
                GUI.contentColor = new Color32(TXTcol[0], TXTcol[1], TXTcol[2], 255);
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();

                if (PhotonNetwork.InRoom)
                {
                    GUILayout.Label($"Room: {(HidingCode ? "-----" : PhotonNetwork.CurrentRoom.Name)}");
                    GUILayout.Label($"Players: {PhotonNetwork.CurrentRoom.PlayerCount}");
                    GUILayout.Label($"Streamer: {PhotonNetwork.LocalPlayer.NickName}");
                }
                else
                {
                    GUILayout.Label("Not Connected");
                }
                GUILayout.Space(5);
                if (PhotonNetwork.InRoom)
                {
                    if (GUILayout.Button("Leave Room")) { PhotonNetwork.Disconnect(); }
                }
                else
                {
                    GUILayout.Label("Room Code:");
                    string input;
                    if (HidingCode)
                    {
                        input = GUILayout.PasswordField(RoomJoin ?? "", '-', 12);
                    }
                    else
                    {
                        input = GUILayout.TextField(RoomJoin ?? "", 12);
                    }
                    RoomJoin = input.ToUpperInvariant();
                    if (GUILayout.Button($"Join {(HidingCode ? "-----" : RoomJoin)}"))
                    {
                        PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(RoomJoin, JoinType.Solo);
                        RoomJoin = "";
                    }
                }
                GUILayout.Space(15);
                if (GUILayout.Button($"Configuration")) { Config = true; Mainshit = false; }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                GUI.DragWindow();
            }

            // BG
            public static string R = "0";
            public static string G = "0";
            public static string B = "0";

            // TXT
            public static string R2 = "255";
            public static string G2 = "255";
            public static string B2 = "255";

            public static void ConfigTab(int i)
            {
                GUI.backgroundColor = new Color32(BGcol[0], BGcol[1], BGcol[2], 255);
                GUI.contentColor = new Color32(TXTcol[0], TXTcol[1], TXTcol[2], 255);

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();

                if (GUILayout.Button($"Back")) { Mainshit = true; Config = false; }

                if (GUILayout.Button($"Hide Code: {(HidingCode ? "Active" : "Unactive")}"))
                {
                    HidingCode = !HidingCode;
                }

                GUILayout.Space(5);

                GUILayout.Label("Background Color:");
                GUILayout.Space(5);
                R = GUILayout.TextField(R, 3);
                G = GUILayout.TextField(G, 3);
                B = GUILayout.TextField(B, 3);

                GUILayout.Space(10);

                GUILayout.Label("Text Color:");
                GUILayout.Space(5); ;
                R2 = GUILayout.TextField(R2, 3);
                G2 = GUILayout.TextField(G2, 3);
                B2 = GUILayout.TextField(B2, 3);

                if (GUILayout.Button("Update UI Colors."))
                {
                    byte Rr = 0; byte Gg = 0; byte Bb = 0;
                    byte.TryParse(R, out Rr); byte.TryParse(G, out Gg); byte.TryParse(B, out Bb);

                    BGcol[0] = Rr;
                    BGcol[1] = Gg;
                    BGcol[2] = Bb;


                    byte Rr2 = 0; byte Gg2 = 0; byte Bb2 = 0;
                    byte.TryParse(R2, out Rr2); byte.TryParse(G2, out Gg2); byte.TryParse(B2, out Bb2);

                    TXTcol[0] = Rr2;
                    TXTcol[1] = Gg2;
                    TXTcol[2] = Bb2;
                }

                if (GUILayout.Button("Reset UI Design"))
                {
                    BGcol = new byte[] { 0, 0, 0 };
                    TXTcol = new byte[] { 255, 255, 255 };
                }

                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                GUI.DragWindow();
            }

        }


    }
}
