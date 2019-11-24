﻿using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1
{
    class CameraManager
    {
        #region Makes me sick

        private List<Vector2> viewPortPos2 = new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(0,450)
        };

        private List<Vector2> viewPortPos3 = new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(800,0),
            new Vector2(400, 450)
        };

        private List<Vector2> viewPortPos4 = new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(800,0),
            new Vector2(0, 450),
            new Vector2(800, 450)
        };

        #endregion


        public static List<iEntity> subjectList = new List<iEntity>();
        private List<Camera> cameraList = new List<Camera>();

        public struct CameraView
        {
            public CameraView(Viewport view, Matrix tran)
            {
                viewPort = view;
                transform = tran;
            }

            public Viewport viewPort;
            public Matrix transform;
        }


        public CameraManager()
        {
        }


        public static void RequestCamera(iEntity entity)
        {
            subjectList.Add(entity);
        }


        public void AddCameras()
        {
            #region Makes me sick x2
            List<Vector2> viewPosList;
            if(subjectList.Count == 2)
            {
                viewPosList = viewPortPos2;
            }
            else if(subjectList.Count == 3)
            {
                viewPosList = viewPortPos3;
            }
            else
            {
                viewPosList = viewPortPos4;
            }
            #endregion

            var resolution = new Vector2(Kernel.ScreenWidth, Kernel.ScreenHeight);

            int subjectCount = 0;
            foreach(var subject in subjectList)
            {
                var camera = new Camera(subject, new Rectangle((int)viewPosList[subjectCount].X, (int)viewPosList[subjectCount].Y, (int)resolution.X / (subjectList.Count <= 2 ? 1 : 2), (int)resolution.Y / 2));
                cameraList.Add(camera);
                subjectCount++;
            }
        }


        public List<CameraView> Update()
        {
            List<CameraView> camView = new List<CameraView>();
            foreach(var camera in cameraList)
            {
                camera.Update();
                camView.Add(new CameraView(camera.viewPort, camera.transform));
            }

            return camView;
        }
    }
}