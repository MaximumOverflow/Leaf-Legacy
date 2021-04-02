; ModuleID = 'Test'
source_filename = "Test"

%struct_Test_Vec2 = type { i8, i8 }
%ref_Test.Vec2 = type { %struct_Test_Vec2*, %__allocator_vtable* }
%__allocator_vtable = type { i8* (i32)*, void (i8*)* }

@singleton_attrib__VarArg = global {} zeroinitializer
@singleton_attrib__External = global {} zeroinitializer

declare i32 @printf(i8*, ...)

define i8 @main() {
  %vec = alloca %struct_Test_Vec2, align 8
  %vec.repack = getelementptr inbounds %struct_Test_Vec2, %struct_Test_Vec2* %vec, i64 0, i32 0
  store i8 42, i8* %vec.repack, align 8
  %vec.repack1 = getelementptr inbounds %struct_Test_Vec2, %struct_Test_Vec2* %vec, i64 0, i32 1
  store i8 64, i8* %vec.repack1, align 1
  %1 = insertvalue %ref_Test.Vec2 undef, %struct_Test_Vec2* %vec, 0
  %2 = insertvalue %ref_Test.Vec2 %1, %__allocator_vtable* null, 1
  call void @function_Test_freeThis_ref_Test.Vec2_void(%ref_Test.Vec2 %2)
  ret i8 0
}

define void @function_Test_freeThis_ref_Test.Vec2_void(%ref_Test.Vec2 %0) {
  %2 = alloca %ref_Test.Vec2, align 8
  %.repack = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %2, i64 0, i32 0
  %.elt = extractvalue %ref_Test.Vec2 %0, 0
  store %struct_Test_Vec2* %.elt, %struct_Test_Vec2** %.repack, align 8
  %.repack1 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %2, i64 0, i32 1
  %.elt2 = extractvalue %ref_Test.Vec2 %0, 1
  store %__allocator_vtable* %.elt2, %__allocator_vtable** %.repack1, align 8
  %free_fn = getelementptr inbounds %__allocator_vtable, %__allocator_vtable* %.elt2, i64 0, i32 1
  %3 = load void (i8*)*, void (i8*)** %free_fn, align 8
  %4 = bitcast %ref_Test.Vec2* %2 to i8**
  %5 = load i8*, i8** %4, align 8
  tail call void %3(i8* %5)
  ret void
}
