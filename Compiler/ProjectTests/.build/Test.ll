; ModuleID = 'Test'
source_filename = "Test"

%struct_Test_Vec2 = type { i8, i8 }

@singleton_attrib__VarArg = global {} zeroinitializer
@singleton_attrib__External = global {} zeroinitializer
@0 = global [9 x i8] c"(%i, %i)\00"

declare i32 @printf(i8*, ...)

define i8 @main() {
  %vec = alloca %struct_Test_Vec2, align 8
  store %struct_Test_Vec2 { i8 42, i8 64 }, %struct_Test_Vec2* %vec, align 1
  %1 = alloca %struct_Test_Vec2*, align 8
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %1, align 8
  %2 = load %struct_Test_Vec2*, %struct_Test_Vec2** %1, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%struct_Test_Vec2* %2)
  ret i8 0
}

define void @function_Test_toString_ref_Test.Vec2_void(%struct_Test_Vec2* %0) {
  %2 = alloca %struct_Test_Vec2*, align 8
  store %struct_Test_Vec2* %0, %struct_Test_Vec2** %2, align 8
  %3 = load %struct_Test_Vec2*, %struct_Test_Vec2** %2, align 8
  %4 = getelementptr inbounds %struct_Test_Vec2, %struct_Test_Vec2* %3, i32 0, i32 0
  %5 = load %struct_Test_Vec2*, %struct_Test_Vec2** %2, align 8
  %6 = getelementptr inbounds %struct_Test_Vec2, %struct_Test_Vec2* %5, i32 0, i32 1
  %7 = load %struct_Test_Vec2*, %struct_Test_Vec2** %2, align 8
  %8 = load i8, i8* %4, align 1
  %9 = load i8, i8* %6, align 1
  %10 = call i32 (i8*, ...) @printf(i8* getelementptr inbounds ([9 x i8], [9 x i8]* @0, i64 0, i64 0), i8 %8, i8 %9)
  ret void
}
