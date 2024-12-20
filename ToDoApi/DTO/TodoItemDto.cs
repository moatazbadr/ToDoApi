﻿namespace ToDoApi.DTO
{
    public class TodoItemDto {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public bool IsDone { get; set; } 
        public DateTime? CreationDate { get; set; }
    }
}
