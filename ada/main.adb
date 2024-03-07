with Ada.Text_IO; use Ada.Text_IO;

procedure Main is

   Id_Of_Thread : Integer := 0;
   Number_Of_Threads : Positive := 2;
   Step : Long_Long_Integer := 2;

   Flag : Boolean := False;
   Can_Stop : Boolean := False;

   pragma Atomic(Can_Stop);
   pragma Volatile(Id_Of_Thread);


   task type Break_Thread;
   task type Main_Thread;

   type Threads_Array is array (Positive range <>) of Main_Thread;

   task body Break_Thread is
   begin
      delay 1.0;
      Can_Stop := True;
   end Break_Thread;

   task body Main_Thread is
      Sum : Long_Long_Integer := 0;
      Steps : Integer := 0;
   begin
      loop
         Steps := Steps + 1;
         Sum := Sum + Step;
         exit when Can_Stop;
      end loop;
      delay 1.0;
      Id_Of_Thread := Id_Of_Thread + 1;
      Put_Line("Thread: " & Id_Of_Thread'Img & " Sum: " & Sum'Img & " Steps: " & 
Steps'Img);

   end Main_Thread;

   procedure Lab1(N: Positive) is
      Threads : Threads_Array(1 .. N);
      Break : Break_Thread;
   begin
      null;
      end Lab1;
begin
   Put("EnteR step value: ");
   Step := Long_Long_Integer'Value(Get_Line);
   Put("Enter number of threads: ");
   Number_Of_Threads := Positive'Value(Get_Line);
   Lab1(Number_Of_Threads);
   null;
end Main;
