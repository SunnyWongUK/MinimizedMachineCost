using System;
using System.Collections.Generic;
using System.Linq;
using Console = System.Diagnostics.Debug;

namespace ConsoleApp1
{
    class CompareMachineTasks : IComparer<MachineTask>
    {
        public int Compare(MachineTask obj1, MachineTask obj2)
        {
            string s1 = obj1.Machine.ID + obj1.Task.ID;
            string s2 = obj2.Machine.ID + obj2.Task.ID;
            return s1.CompareTo(s2);
        }
    }

    class Machine
    {
        public string ID { get; set; }
    }

    class Task
    {
        public string ID { get; set; }
    }

    class MachineTask
    {
        public Machine Machine { get; set; }
        public Task Task { get; set; }
        public Double Cost { get; set; }
    }

    class Program
    {
        public static void FindPlanMachineTask(int machineNo, int machineNoLimit, List<MachineTask> machineTasks, ref List<List<MachineTask>> planMachineTasks, ref List<MachineTask> planMachineTask)
        {
            if (machineNo > machineNoLimit)
            {
                planMachineTasks.Add(planMachineTask);
                planMachineTask = new List<MachineTask>();
            }
            else
            {
                List<MachineTask> l = machineTasks.Where(m => m.Machine.ID == machineNo.ToString()).ToList();
                for (int j = 0; j < l.Count; j++)
                {
                    planMachineTask.Add(l[j]);                    
                    FindPlanMachineTask(machineNo + 1, machineNoLimit, machineTasks, ref planMachineTasks, ref planMachineTask);
                }
            }
        }

        public static string ArrayChallenge(string[] strArr)
        {
            List<string> s = strArr.ToList();
            List<MachineTask> machineTasks = new List<MachineTask>();
            Machine machine;
            Task task;
            MachineTask machineTask;
            double totalCost;
            double MinTotalCost;
            int index;
            int machineNo;
            List<List<MachineTask>> planMachineTasks;
            List<MachineTask> planMachineTask;
            string temp;

            machineNo = s.Count;
            machineTasks = new List<MachineTask>();
            for (int i = 0; i < s.Count; i++)
            {
                machine = new Machine() { ID = (i + 1).ToString() };
                var l = s[i].Replace("(", "").Replace(")", "").Split(",");
                for (int j = 0; j < l.Length; j++)
                {
                    task = new Task() { ID = (j + 1).ToString() };
                    machineTask = new MachineTask() { Machine = machine, Task = task, Cost = Convert.ToDouble(l[j]) };
                    index = machineTasks.BinarySearch(machineTask, new CompareMachineTasks());
                    if (index < 0)
                    {
                        machineTasks.Insert(~index, machineTask);
                    }
                }
            }
            planMachineTasks = new List<List<MachineTask>>();
            planMachineTask = new List<MachineTask>();
            FindPlanMachineTask(1, machineNo, machineTasks, ref planMachineTasks, ref planMachineTask);

            MinTotalCost = int.MaxValue;
            for (int i = 0; i < planMachineTasks.Count; i++)
            {
                totalCost = 0;
                for (int j = 0; j < planMachineTasks[i].Count; j++)
                {
                    totalCost = totalCost + planMachineTasks[i][j].Cost;
                }
                if (totalCost < MinTotalCost)
                {
                    MinTotalCost = totalCost;
                    planMachineTask = planMachineTasks[i];
                }
            }

            temp = "";
            for (int i = 0; i < planMachineTask.Count; i++)
            {
                temp = temp + "(" + planMachineTask[i].Machine.ID + "," + planMachineTask[i].Task.ID + ")";
            }

            return temp;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(ArrayChallenge(new string[] { "(1,2,1)", "(4,1,5)", "(5,2,1)" }));
        }
    }
}
