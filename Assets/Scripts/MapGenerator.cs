using System;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    private static int size = 20; // 그리드 크기
    private static int roomCount = 15; // 생성할 방의 개수
    private static int[,] stage;
    private static System.Random rand = new System.Random();
    private static int[] dx = { 0, 1, 0, -1 }; // 오른쪽, 아래, 왼쪽, 위쪽
    private static int[] dy = { 1, 0, -1, 0 };

    void Start()
    {
        GenerateStage();
        PrintStage();
        var farthestRoom = FindFarthestRoom();
        Debug.Log($"가장 먼 방: ({farthestRoom.Item1}, {farthestRoom.Item2})");
    }

    private static void GenerateStage()
    {
        // 스테이지 초기화
        stage = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                stage[i, j] = -1; // 방이 없는 상태
            }
        }

        // 중심에서 시작
        int centerX = size / 2;
        int centerY = size / 2;
        stage[centerX, centerY] = 0;
        List<Tuple<int, int>> rooms = new List<Tuple<int, int>>();
        rooms.Add(new Tuple<int, int>(centerX, centerY));
        
        int currentRoom = 1;

        // 방 생성
        while (currentRoom < roomCount)
        {
            // 더 이상 방을 추가할 수 없는 경우 중단
            if (rooms.Count == 0)
            {
                Debug.Log("더 이상 방을 추가할 수 없습니다.");
                break;
            }

            // 랜덤한 방 선택
            var room = rooms[rand.Next(rooms.Count)];
            int x = room.Item1;
            int y = room.Item2;

            // 방이 있는 공간 확인
            List<int> directions = new List<int> { 0, 1, 2, 3 };
            bool createdChild = false;
            while (directions.Count > 0)
            {
                int dirIndex = rand.Next(directions.Count);
                int dir = directions[dirIndex];
                directions.RemoveAt(dirIndex);

                int nx = x + dx[dir];
                int ny = y + dy[dir];

                // 새로운 방이 범위 내에 있고, 인접한 칸이 다른 방과 접하지 않는지 확인
                if (IsInBounds(nx, ny) && stage[nx, ny] == -1 && !IsAdjacentToRoom(nx, ny))
                {
                    stage[nx, ny] = currentRoom;
                    rooms.Add(new Tuple<int, int>(nx, ny));
                    currentRoom++;
                    createdChild = true;
                    break;
                }
            }

            // 방을 생성하지 못했다면 리스트에서 제거
            if (!createdChild)
            {
                rooms.Remove(room);
            }
        }
    }

    private static bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < size && y >= 0 && y < size;
    }

    private static bool IsAdjacentToRoom(int x, int y)
    {
        for (int dir = 0; dir < 4; dir++)
        {
            int nx = x + dx[dir];
            int ny = y + dy[dir];
            if (IsInBounds(nx, ny) && stage[nx, ny] != -1)
            {
                return true;
            }
        }
        return false;
    }

    private static void PrintStage()
    {
        string stageString = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (stage[i, j] == -1)
                {
                    stageString += "██";
                }
                else
                {
                    stageString += $"{stage[i, j]:D2} ";
                }
            }
            stageString += "\n";
        }
        Debug.Log(stageString);
    }

    private static Tuple<int, int> FindFarthestRoom()
    {
        int centerX = size / 2;
        int centerY = size / 2;
        int[,] distances = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                distances[i, j] = int.MaxValue;
            }
        }

        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(new Tuple<int, int>(centerX, centerY));
        distances[centerX, centerY] = 0;

        Tuple<int, int> farthestRoom = new Tuple<int, int>(centerX, centerY);
        int maxDistance = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int x = current.Item1;
            int y = current.Item2;
            int currentDistance = distances[x, y];

            for (int dir = 0; dir < 4; dir++)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];

                if (IsInBounds(nx, ny) && stage[nx, ny] != -1 && distances[nx, ny] == int.MaxValue)
                {
                    distances[nx, ny] = currentDistance + 1;
                    queue.Enqueue(new Tuple<int, int>(nx, ny));

                    if (distances[nx, ny] > maxDistance)
                    {
                        maxDistance = distances[nx, ny];
                        farthestRoom = new Tuple<int, int>(nx, ny);
                    }
                }
            }
        }

        return farthestRoom;
    }
}
