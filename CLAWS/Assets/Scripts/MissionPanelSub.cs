// Last Edited: 3-31-19
using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour {
    public GameObject panel;
    // public TextMesh goalText;
    // public TextMesh prevTaskText;
    // public TextMesh curTaskText;
    // public TextMesh nextTaskText;
    public TextAsset missionFile;
    public Mission m;

    public class Mission {
        //public TextAsset missionfile;
        // Sturct and Enum used by Mission
        public class Mission_Subtask {
            string text;
            bool complete;
            // TO DO: Insert Navigational Component
            public Mission_Subtask(string text_in){
                text = text_in;
                complete = false;
            }
            public string get_text(){
                return text;
            }
            public bool get_status(){
                return complete;
            }
            public void mark_complete(){
                complete = true;
            }
            public void mark_incomplete(){
                complete = false;
            }
            public bool toggle(){
                if(complete){
                    mark_incomplete();
                    return false;
                } else {
                    mark_complete();
                    return true;
                }
            }
        }

        public class Mission_Task {
            string text;
            bool complete;
            Mission_Subtask[] subtasks;
            int subtasks_complete;
            // TO DO: Insert Navigational Component
            public Mission_Task(string text_in, Mission_Subtask[] subtasks_in){
                text = text_in;
                complete = false;
                subtasks = subtasks_in;
                subtasks_complete = 0;
            }
            public Mission_Task(string text_in){
                text = text_in;
                complete = false;
                subtasks_complete = 0;
                subtasks = new Mission_Subtask[0]; 
            }
            public ref Mission_Subtask get_prev_subtask(){
                return ref subtasks[subtasks_complete - 1];
            }
            public ref Mission_Subtask get_cur_subtask(){
                return ref subtasks[subtasks_complete];
            }
            public ref Mission_Subtask get_next_subtask(){
                return ref subtasks[subtasks_complete + 1];
            }
            public ref Mission_Subtask get_first_subtask(){
                return ref subtasks[0];
            }
            public ref Mission_Subtask get_last_subtask(){
                return ref subtasks[subtasks.Length - 1];
            }
            public ref Mission_Subtask[] get_subtasks() {
                return ref subtasks;
            }
            public int get_subtasks_length(){
                return subtasks.Length;
            }
            public ref int get_subtasks_complete() {
                return ref subtasks_complete;
            }
            public string get_text(){
                return text;
            }
            public bool get_status(){
                return complete;
            }
            public void mark_complete(){
                complete = true;
            }
            public void mark_incomplete(){
                complete = false;
            }
            public bool toggle(){
                bool ret = subtasks[subtasks_complete].toggle();
                if(ret){
                    subtasks_complete++;
                } else {
                    subtasks_complete--;
                }
                if(subtasks_complete < subtasks.Length){
                    mark_incomplete();
                    return false;
                } else { //if (subtasks_complete == subtasks.Length){
                    mark_complete();
                    return true;
                }
            }
            public void mark_subtask_complete(){
                subtasks[subtasks_complete].mark_complete();
                subtasks_complete++;
                if(subtasks_complete < subtasks.Length){
                    mark_incomplete();
                } else if (subtasks_complete == subtasks.Length){
                    mark_complete();
                }
            }
            public void mark_subtask_incomplete(){
                subtasks[subtasks_complete].mark_incomplete();
                subtasks_complete--;
                mark_incomplete();
            }
        }
        public class Mission_Phase {
            string text;
            bool complete;
            Mission_Task[] tasks;
            int tasks_complete;
            // TO DO: Insert Navigational Component
            public Mission_Phase(string text_in, Mission_Task[] tasks_in){
                text = text_in;
                complete = false;
                tasks = tasks_in;
                tasks_complete = 0;
            }
            public Mission_Phase(string text_in){
                text = text_in;
                complete = false;
                tasks_complete = 0;
                tasks = new Mission_Task[0];
            }
            public ref Mission_Task get_prev_task(){
                return ref tasks[tasks_complete - 1];
            }
            public ref Mission_Task get_cur_task(){
                return ref tasks[tasks_complete];
            }
            public ref Mission_Task get_next_task(){
                return ref tasks[tasks_complete + 1];
            }
            public ref Mission_Task get_first_task(){
                return ref tasks[0];
            }
            public ref Mission_Task get_last_task(){
                return ref tasks[tasks.Length - 1];
            }
            public ref Mission_Task[] get_tasks(){
                return ref tasks;
            }
            public int get_tasks_length() {
                return tasks.Length;
            }
            public ref int get_tasks_complete() {
                return ref tasks_complete;
            }
            public string get_text(){
                return text;
            }
            public bool get_status(){
                return complete;
            }
            public void mark_complete(){
                complete = true;
            }
            public void mark_incomplete(){
                complete = false;
            }
            public void toggle(){
                bool ret = tasks[tasks_complete].toggle();
                if(ret){
                    tasks_complete++;
                } else {
                    tasks_complete--;
                }
                if(tasks_complete < tasks.Length){
                    mark_incomplete();
                } else if (tasks_complete == tasks.Length){
                    mark_complete();
                }
            }
            public void mark_subtask_complete(){
                tasks[tasks_complete].mark_subtask_complete();
                if(tasks[tasks_complete].get_subtasks_complete() == tasks[tasks_complete].get_subtasks_length()){
                    tasks_complete++;
                }
                if(tasks_complete < tasks.Length){
                    mark_incomplete();
                } else if (tasks_complete == tasks.Length){
                    mark_complete();
                }
            }
            public void mark_subtask_incomplete(){
                tasks[tasks_complete].mark_subtask_incomplete();
                mark_incomplete();
            }
        }
        public enum Mission_Type : int {
            Repair,
            Experiment,
            Other
            // TO DO: Insert more here
        }

        // Mission Variables
        string title;
        string goal;
        Mission_Phase[] phases;
        Mission_Type type;
        float progress;
        int phases_complete;
        int total_subtasks;
        int num_subtasks_complete;

        // Mission Contructors
        public Mission() {
            title = "Repair the ISS";
            goal = "Fix parts of the ISS";
            type = Mission_Type.Repair;
            phases_complete = 0; 
            progress = 0;
            total_subtasks = 0;
            num_subtasks_complete = 0;
            // Populate task array
            Mission_Subtask[] s = new Mission_Subtask[5];
            for(int i = 0; i < 5; i++){
                s[i] = new Mission_Subtask("Subtask " + i);
                total_subtasks++;
            }
            Mission_Task[] t = new Mission_Task[5];
            for(int i = 0; i < 5; i++){
                t[i] = new Mission_Task("Task " + i, s);
            }
            phases = new Mission_Phase[5];
            for(int i = 0; i < 5; i++){
                phases[i] = new Mission_Phase("Phase " + i, t);
            }
        }
        public Mission(string title_in, string goal_in, string type_in, 
                        Mission_Phase[] phase_in, int total_subtasks_in) {
            title = title_in;
            goal = goal_in;
            phases_complete = 0; 
            progress = 0;
            num_subtasks_complete = 0;
            if(type_in == "Repair"){
                type = Mission_Type.Repair;
            } else if (type_in == "Experiment") {
                type = Mission_Type.Experiment;
            } else {
                type = Mission_Type.Other;
            }
            phases = phase_in;
            total_subtasks = total_subtasks_in;
        }
        public Mission(TextAsset missionFile) {
            string missionText = missionFile.text;
            string[] lines = System.Text.RegularExpressions.Regex.Split(missionText, "\n");
     
            //string[] lines = System.IO.File.ReadAllLines(missionFile);

            title = lines[0];
            goal = lines[1];
            phases_complete = 0; 
            progress = 0;
            total_subtasks = 0;
            num_subtasks_complete = 0;

            if(lines[2] == "Repair"){
                type = Mission_Type.Repair;
            } else if (lines[2] == "Experiment") {
                type = Mission_Type.Experiment;
            } else {
                type = Mission_Type.Other;
            }

            // Populate task array
            int numPhases = int.Parse(lines[3]);
            phases = new Mission_Phase[numPhases];
            int it = 4;
            for(int i = 0; i < phases.Length; i++){
                phases[i] = new Mission_Phase(lines[it]);
                it++;

                int numTasks = int.Parse(lines[it]);
                it++;
                phases[i].get_tasks() = new Mission_Task[numTasks];
                for(int k = 0; k < phases[i].get_tasks().Length; k++){
                    phases[i].get_tasks()[k] = new Mission_Task(lines[it]);
                    it++;

                    int numSubtasks = int.Parse(lines[it]);
                    it++;
                    phases[i].get_tasks()[k].get_subtasks() = new Mission_Subtask[numSubtasks];
                    for(int j = 0; j < phases[i].get_tasks()[k].get_subtasks_length(); j++){
                        phases[i].get_tasks()[k].get_subtasks()[j] = new Mission_Subtask(lines[it]);
                        it++;
                        total_subtasks++;
                    }
                }
            }
        }

        public string get_title(){
            return title;
        }
        public string get_goal(){
            return goal;
        }
        // TO DO: Change so it returns these tasks rather than printing
        
        public void print_tasks(){
            for(int i = 0; i < phases.Length; i++){
                Console.WriteLine(phases[i].get_text() + " - " + phases[i].get_status());
                
                for(int k = 0; k < phases[i].get_tasks_length(); k++){
                    Console.WriteLine("\t" + phases[i].get_tasks()[k].get_text() + " - " + phases[i].get_tasks()[k].get_status());

                    for(int j = 0; j < phases[i].get_tasks()[k].get_subtasks_length(); j++){
                        Console.WriteLine("\t\t" + phases[i].get_tasks()[k].get_subtasks()[j].get_text() + " - " + phases[i].get_tasks()[k].get_subtasks()[j].get_status());
                    }
                }
            }
        }
        public ref Mission_Subtask get_cur_subtask(){
            return ref phases[phases_complete].get_cur_task().get_cur_subtask();
        }
        public ref Mission_Subtask get_prev_subtask(){
            if(phases[phases_complete].get_cur_task().get_subtasks_complete() > 0)
                return ref phases[phases_complete].get_cur_task().get_prev_subtask();
            else if(phases[phases_complete].get_tasks_complete() > 0)
                return ref phases[phases_complete].get_prev_task().get_last_subtask();
            else //if(phases_complete > 0)
                return ref phases[phases_complete - 1].get_last_task().get_last_subtask();
            
        }
        public ref Mission_Subtask get_next_subtask(){
            if(phases[phases_complete].get_cur_task().get_subtasks_complete() < 
                    phases[phases_complete].get_cur_task().get_subtasks_length())
                return ref phases[phases_complete].get_cur_task().get_next_subtask();
            else if(phases[phases_complete].get_tasks_complete() < phases[phases_complete].get_tasks_length())
                return ref phases[phases_complete].get_next_task().get_first_subtask();
            else //if(phases_complete < phases.Length)
                return ref phases[phases_complete + 1].get_first_task().get_first_subtask();
        
        }
        // public Mission_Task get_task(int task_num){
        //     // Note: Stored in array zero-based, but task_num is one based
        //     return tasks[task_num - 1];
        // }
        public string get_type(){
            if(type == Mission_Type.Repair){
                return "Repair";
            } else if(type == Mission_Type.Experiment){
                return "Experiment";
            } else {
                return "Other";
            }
        }
        public double get_progress(){
            return progress;
        }
        public void mark_subtask_complete(){
            phases[phases_complete].mark_subtask_complete();
            num_subtasks_complete++;

            // If phase marked completed, increment
            if(phases[phases_complete].get_status() == true){
                phases_complete++;
            }
            
            // Update progress
            progress = (float)num_subtasks_complete / (float)total_subtasks;
        }
        public void mark_subtask_incomplete(){
            phases[phases_complete].mark_subtask_incomplete();
            num_subtasks_complete--;
            
            // Update progress
            progress = (float)num_subtasks_complete / (float)total_subtasks;
        }
        // public void toggle_current_task_status(){
        //     toggle_task_status(tasks_complete);
        // }
        public int num_phases_complete(){
            return phases_complete;
        }
        public int num_phases(){
            return phases.Length;
        }

    }
    
    void Start(){
        //Text objText = GetComponent<Text>(); //
        GameObject goalMesh = panel.transform.GetChild(0).gameObject;
        GameObject prevMesh = panel.transform.GetChild(1).gameObject;
        GameObject curMesh = panel.transform.GetChild(2).gameObject;
        GameObject nextMesh = panel.transform.GetChild(3).gameObject;

        m = new Mission(missionFile);
        goalMesh.GetComponent<Text>().text = m.get_title();
        Update_Tasks_List();
    }
    void Update_Tasks_List(){
        if(m.num_tasks_complete() > 0)
            prevMesh.GetComponent<Text>().text = m.get_prev_task().get_name() + ": " + m.get_prev_task().get_info();
        curMesh.GetComponent<Text>().text = m.get_cur_task().get_name() + ": " + m.get_cur_task().get_info();
        if(m.num_tasks_complete() < m.num_tasks())
            nextMesh.GetComponent<Text>().text = m.get_next_task().get_name() + ": " + m.get_next_task().get_info();
    }
    void Mark_Complete_Voice(){
        m.toggle_status();
        Update_Tasks_List();
    }
    void Mark_Incomplete_Voice(){
        m.toggle_status();
        Update_Tasks_List();
    }
    void Go_To_Task_Voice(int task_num){
        if(m.task_num - 1 > 0)
            prevMesh.GetComponent<Text>().text = m.get_task(task_num - 1).get_name() + ": " + m.get_task(task_num - 1).get_info();
        curMesh.GetComponent<Text>().text = m.get_task(task_num).get_name() + ": " + m.get_task(task_num).get_info();
        if(m.task_num < m.num_tasks())
            nextMesh.GetComponent<Text>().text = m.get_task(task_num + 1).get_name() + ": " + m.get_task(task_num + 1).get_info();   
    }
    void Return_To_Current_Task_Voice(){
         Update_Tasks_List();
    }
    void Flag(){

    }
    void Unflag(){

    }
}

