//By: Akhier Dragonheart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libtcod;
using CSharpHelperCode;
using CSharpSimpleMapGen;
//using CSharpDijkstraAlgorithm;
namespace CSharpShowDijkstraWorking {
    class Program {
        const int windowWidth = 20, windowHeight = 20;
        static void Main(string[] args) {
            TCODConsole.initRoot(windowWidth, windowHeight, "Showing Dijkstra Algorithm Working");
            TCODSystem.setFps(30);
            Graph map = makeMap();
            bool closeWindow = false;
            TCODMouseData mData = TCODMouse.getStatus();
            List<Vector2D> path = new List<Vector2D>();
            bool newPath = false;
            do {
                if (newPath) {
                    drawMap(map.AllNodes, path, true);
                    newPath = !map.calculateShortestPath();
                }
                drawMap(map.AllNodes, path, false);
                mData = TCODMouse.getStatus();
                if (mData.RightButtonPressed) {
                    Vector2D rVector = getVector(mData.PixelX, mData.PixelY, map.AllNodes);
                    if (rVector != null) {
                        map.SourceVector = rVector;
                        newPath = true;
                    }
                }
                else if (mData.MiddleButtonPressed) {
                    map = makeMap();
                    path = new List<Vector2D>();
                }
                else if (mData.LeftButtonPressed) {
                    if ((mData.PixelX / 8 >= windowWidth - 7) && (mData.PixelY / 8 == 0)) {
                        closeWindow = true;
                    }
                }
                Vector2D lVector = getVector(mData.PixelX, mData.PixelY, map.AllNodes);
                if ((lVector != null)&&(map.SourceVector != lVector)&&(map.SourceVector != null)) {
                    path = map.retrieveShortestPath(lVector);
                }
            } while (!closeWindow);
        }
        static Graph makeMap() {
            Graph output = new Graph();
            bool[,] boolMap = MapGen.newMap(windowWidth, windowHeight, true);
            Vector2D[,] vectorMap = new Vector2D[windowWidth, windowHeight];
            for (int column = 0; column < windowHeight; column++) {
                for (int row = 0; row < windowWidth; row++) {
                    if (boolMap[row, column]) {
                        vectorMap[row, column] = new Vector2D(row, column, false);
                        output.addVector(vectorMap[row, column]);
                    }
                }
            }
            for (int column = 1; column < windowHeight-1; column++) {
                for (int row = 1; row < windowWidth-1; row++) {
                    if (boolMap[row, column]) {
                        if (boolMap[row - 1, column + 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row - 1, column + 1], 11)); }
                        if (boolMap[row, column + 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row, column + 1], 10)); }
                        if (boolMap[row + 1, column]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row + 1, column], 10)); }
                        if (boolMap[row + 1, column + 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row + 1, column + 1], 11)); }
                    }
                }
            }
            return output;
        }
        static Vector2D getVector(int windowx, int windowy, List<Vector2D> vectormap) {
            int X = windowx / 8;
            int Y = windowy / 8;
            foreach (Vector2D tile in vectormap) {
                if (tile.X == X && tile.Y == Y) {
                    return tile;
                }
            }
            return null;
        }
        static void drawMap(List<Vector2D> floormap, List<Vector2D> path, bool invertcolor) {
            if (!invertcolor) {
                TCODConsole.root.setBackgroundColor(new TCODColor(15, 15, 15));
                TCODConsole.root.setForegroundColor(TCODColor.lightestGrey);
            }
            else {
                TCODConsole.root.setForegroundColor(new TCODColor(15, 15, 15));
                TCODConsole.root.setBackgroundColor(TCODColor.lightestGrey);
            }
            TCODConsole.root.clear();
            for (int column = 0; column < windowHeight; column++) {
                for (int row = 0; row < windowWidth; row++) {
                    TCODConsole.root.putChar(row, column, '#');
                }
            }
            foreach (Vector2D tile in floormap) {
                TCODConsole.root.putChar(tile.X, tile.Y, '.');
            }
            int redValue = 0, greenValue = 255, stepSize = (path.Count != 0)? 510 / path.Count : 1;
            foreach (Vector2D tile in path) {
                TCODConsole.root.setCharBackground(tile.X, tile.Y, new TCODColor(redValue, greenValue, 0));
                if (redValue < 255) {
                    redValue += stepSize;
                    if (redValue > 255) {
                        greenValue -= redValue - 255;
                        redValue = 255;
                    }
                }
                else if (greenValue > 0) {
                    greenValue -= stepSize;
                    if (greenValue < 0) {
                        greenValue = 0;
                    }
                }
            }
            TCODConsole.root.print(windowWidth - 7, 0, "Close X");
            TCODConsole.flush();
        }
    }
}
