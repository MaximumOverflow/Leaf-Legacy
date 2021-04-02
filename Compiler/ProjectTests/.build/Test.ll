; ModuleID = 'Test'
source_filename = "Test"

%struct_Test_Vec2 = type { i8, i8 }
%ref_Test.Vec2 = type { %struct_Test_Vec2*, %__allocator_vtable* }
%__allocator_vtable = type { i8* (i32)*, void (i8*)* }

@singleton_attrib__VarArg = global {} zeroinitializer
@singleton_attrib__External = global {} zeroinitializer
@0 = global [9 x i8] c"(%i, %i)\00"

declare i32 @printf(i8*, ...)

define i8 @main() {
  %vec = alloca %struct_Test_Vec2, align 8
  store %struct_Test_Vec2 { i8 42, i8 64 }, %struct_Test_Vec2* %vec, align 1
  %1 = alloca %ref_Test.Vec2, align 8
  %2 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %1, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %2, align 8
  %3 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %1, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %3, align 8
  %4 = alloca %ref_Test.Vec2, align 8
  %5 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %4, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %5, align 8
  %6 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %4, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %6, align 8
  %7 = load %ref_Test.Vec2, %ref_Test.Vec2* %4, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %7)
  %8 = alloca %ref_Test.Vec2, align 8
  %9 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %8, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %9, align 8
  %10 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %8, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %10, align 8
  %11 = alloca %ref_Test.Vec2, align 8
  %12 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %11, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %12, align 8
  %13 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %11, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %13, align 8
  %14 = load %ref_Test.Vec2, %ref_Test.Vec2* %11, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %14)
  %15 = alloca %ref_Test.Vec2, align 8
  %16 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %15, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %16, align 8
  %17 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %15, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %17, align 8
  %18 = alloca %ref_Test.Vec2, align 8
  %19 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %18, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %19, align 8
  %20 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %18, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %20, align 8
  %21 = load %ref_Test.Vec2, %ref_Test.Vec2* %18, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %21)
  %22 = alloca %ref_Test.Vec2, align 8
  %23 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %22, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %23, align 8
  %24 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %22, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %24, align 8
  %25 = alloca %ref_Test.Vec2, align 8
  %26 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %25, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %26, align 8
  %27 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %25, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %27, align 8
  %28 = load %ref_Test.Vec2, %ref_Test.Vec2* %25, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %28)
  %29 = alloca %ref_Test.Vec2, align 8
  %30 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %29, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %30, align 8
  %31 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %29, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %31, align 8
  %32 = alloca %ref_Test.Vec2, align 8
  %33 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %32, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %33, align 8
  %34 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %32, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %34, align 8
  %35 = load %ref_Test.Vec2, %ref_Test.Vec2* %32, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %35)
  %36 = alloca %ref_Test.Vec2, align 8
  %37 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %36, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %37, align 8
  %38 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %36, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %38, align 8
  %39 = alloca %ref_Test.Vec2, align 8
  %40 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %39, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %40, align 8
  %41 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %39, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %41, align 8
  %42 = load %ref_Test.Vec2, %ref_Test.Vec2* %39, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %42)
  %43 = alloca %ref_Test.Vec2, align 8
  %44 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %43, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %44, align 8
  %45 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %43, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %45, align 8
  %46 = alloca %ref_Test.Vec2, align 8
  %47 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %46, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %47, align 8
  %48 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %46, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %48, align 8
  %49 = load %ref_Test.Vec2, %ref_Test.Vec2* %46, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %49)
  %50 = alloca %ref_Test.Vec2, align 8
  %51 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %50, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %51, align 8
  %52 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %50, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %52, align 8
  %53 = alloca %ref_Test.Vec2, align 8
  %54 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %53, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %54, align 8
  %55 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %53, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %55, align 8
  %56 = load %ref_Test.Vec2, %ref_Test.Vec2* %53, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %56)
  %57 = alloca %ref_Test.Vec2, align 8
  %58 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %57, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %58, align 8
  %59 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %57, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %59, align 8
  %60 = alloca %ref_Test.Vec2, align 8
  %61 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %60, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %61, align 8
  %62 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %60, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %62, align 8
  %63 = load %ref_Test.Vec2, %ref_Test.Vec2* %60, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %63)
  %64 = alloca %ref_Test.Vec2, align 8
  %65 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %64, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %65, align 8
  %66 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %64, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %66, align 8
  %67 = alloca %ref_Test.Vec2, align 8
  %68 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %67, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %68, align 8
  %69 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %67, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %69, align 8
  %70 = load %ref_Test.Vec2, %ref_Test.Vec2* %67, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %70)
  %71 = alloca %ref_Test.Vec2, align 8
  %72 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %71, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %72, align 8
  %73 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %71, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %73, align 8
  %74 = alloca %ref_Test.Vec2, align 8
  %75 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %74, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %75, align 8
  %76 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %74, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %76, align 8
  %77 = load %ref_Test.Vec2, %ref_Test.Vec2* %74, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %77)
  %78 = alloca %ref_Test.Vec2, align 8
  %79 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %78, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %79, align 8
  %80 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %78, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %80, align 8
  %81 = alloca %ref_Test.Vec2, align 8
  %82 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %81, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %82, align 8
  %83 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %81, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %83, align 8
  %84 = load %ref_Test.Vec2, %ref_Test.Vec2* %81, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %84)
  %85 = alloca %ref_Test.Vec2, align 8
  %86 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %85, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %86, align 8
  %87 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %85, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %87, align 8
  %88 = alloca %ref_Test.Vec2, align 8
  %89 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %88, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %89, align 8
  %90 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %88, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %90, align 8
  %91 = load %ref_Test.Vec2, %ref_Test.Vec2* %88, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %91)
  %92 = alloca %ref_Test.Vec2, align 8
  %93 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %92, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %93, align 8
  %94 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %92, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %94, align 8
  %95 = alloca %ref_Test.Vec2, align 8
  %96 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %95, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %96, align 8
  %97 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %95, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %97, align 8
  %98 = load %ref_Test.Vec2, %ref_Test.Vec2* %95, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %98)
  %99 = alloca %ref_Test.Vec2, align 8
  %100 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %99, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %100, align 8
  %101 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %99, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %101, align 8
  %102 = alloca %ref_Test.Vec2, align 8
  %103 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %102, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %103, align 8
  %104 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %102, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %104, align 8
  %105 = load %ref_Test.Vec2, %ref_Test.Vec2* %102, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %105)
  %106 = alloca %ref_Test.Vec2, align 8
  %107 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %106, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %107, align 8
  %108 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %106, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %108, align 8
  %109 = alloca %ref_Test.Vec2, align 8
  %110 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %109, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %110, align 8
  %111 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %109, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %111, align 8
  %112 = load %ref_Test.Vec2, %ref_Test.Vec2* %109, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %112)
  %113 = alloca %ref_Test.Vec2, align 8
  %114 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %113, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %114, align 8
  %115 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %113, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %115, align 8
  %116 = alloca %ref_Test.Vec2, align 8
  %117 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %116, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %117, align 8
  %118 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %116, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %118, align 8
  %119 = load %ref_Test.Vec2, %ref_Test.Vec2* %116, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %119)
  %120 = alloca %ref_Test.Vec2, align 8
  %121 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %120, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %121, align 8
  %122 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %120, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %122, align 8
  %123 = alloca %ref_Test.Vec2, align 8
  %124 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %123, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %124, align 8
  %125 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %123, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %125, align 8
  %126 = load %ref_Test.Vec2, %ref_Test.Vec2* %123, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %126)
  %127 = alloca %ref_Test.Vec2, align 8
  %128 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %127, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %128, align 8
  %129 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %127, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %129, align 8
  %130 = alloca %ref_Test.Vec2, align 8
  %131 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %130, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %131, align 8
  %132 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %130, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %132, align 8
  %133 = load %ref_Test.Vec2, %ref_Test.Vec2* %130, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %133)
  %134 = alloca %ref_Test.Vec2, align 8
  %135 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %134, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %135, align 8
  %136 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %134, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %136, align 8
  %137 = alloca %ref_Test.Vec2, align 8
  %138 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %137, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %138, align 8
  %139 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %137, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %139, align 8
  %140 = load %ref_Test.Vec2, %ref_Test.Vec2* %137, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %140)
  %141 = alloca %ref_Test.Vec2, align 8
  %142 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %141, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %142, align 8
  %143 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %141, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %143, align 8
  %144 = alloca %ref_Test.Vec2, align 8
  %145 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %144, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %145, align 8
  %146 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %144, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %146, align 8
  %147 = load %ref_Test.Vec2, %ref_Test.Vec2* %144, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %147)
  %148 = alloca %ref_Test.Vec2, align 8
  %149 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %148, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %149, align 8
  %150 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %148, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %150, align 8
  %151 = alloca %ref_Test.Vec2, align 8
  %152 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %151, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %152, align 8
  %153 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %151, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %153, align 8
  %154 = load %ref_Test.Vec2, %ref_Test.Vec2* %151, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %154)
  %155 = alloca %ref_Test.Vec2, align 8
  %156 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %155, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %156, align 8
  %157 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %155, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %157, align 8
  %158 = alloca %ref_Test.Vec2, align 8
  %159 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %158, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %159, align 8
  %160 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %158, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %160, align 8
  %161 = load %ref_Test.Vec2, %ref_Test.Vec2* %158, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %161)
  %162 = alloca %ref_Test.Vec2, align 8
  %163 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %162, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %163, align 8
  %164 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %162, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %164, align 8
  %165 = alloca %ref_Test.Vec2, align 8
  %166 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %165, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %166, align 8
  %167 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %165, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %167, align 8
  %168 = load %ref_Test.Vec2, %ref_Test.Vec2* %165, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %168)
  %169 = alloca %ref_Test.Vec2, align 8
  %170 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %169, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %170, align 8
  %171 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %169, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %171, align 8
  %172 = alloca %ref_Test.Vec2, align 8
  %173 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %172, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %173, align 8
  %174 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %172, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %174, align 8
  %175 = load %ref_Test.Vec2, %ref_Test.Vec2* %172, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %175)
  %176 = alloca %ref_Test.Vec2, align 8
  %177 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %176, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %177, align 8
  %178 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %176, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %178, align 8
  %179 = alloca %ref_Test.Vec2, align 8
  %180 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %179, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %180, align 8
  %181 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %179, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %181, align 8
  %182 = load %ref_Test.Vec2, %ref_Test.Vec2* %179, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %182)
  %183 = alloca %ref_Test.Vec2, align 8
  %184 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %183, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %184, align 8
  %185 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %183, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %185, align 8
  %186 = alloca %ref_Test.Vec2, align 8
  %187 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %186, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %187, align 8
  %188 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %186, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %188, align 8
  %189 = load %ref_Test.Vec2, %ref_Test.Vec2* %186, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %189)
  %190 = alloca %ref_Test.Vec2, align 8
  %191 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %190, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %191, align 8
  %192 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %190, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %192, align 8
  %193 = alloca %ref_Test.Vec2, align 8
  %194 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %193, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %194, align 8
  %195 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %193, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %195, align 8
  %196 = load %ref_Test.Vec2, %ref_Test.Vec2* %193, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %196)
  %197 = alloca %ref_Test.Vec2, align 8
  %198 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %197, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %198, align 8
  %199 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %197, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %199, align 8
  %200 = alloca %ref_Test.Vec2, align 8
  %201 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %200, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %201, align 8
  %202 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %200, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %202, align 8
  %203 = load %ref_Test.Vec2, %ref_Test.Vec2* %200, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %203)
  %204 = alloca %ref_Test.Vec2, align 8
  %205 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %204, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %205, align 8
  %206 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %204, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %206, align 8
  %207 = alloca %ref_Test.Vec2, align 8
  %208 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %207, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %208, align 8
  %209 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %207, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %209, align 8
  %210 = load %ref_Test.Vec2, %ref_Test.Vec2* %207, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %210)
  %211 = alloca %ref_Test.Vec2, align 8
  %212 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %211, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %212, align 8
  %213 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %211, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %213, align 8
  %214 = alloca %ref_Test.Vec2, align 8
  %215 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %214, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %215, align 8
  %216 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %214, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %216, align 8
  %217 = load %ref_Test.Vec2, %ref_Test.Vec2* %214, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %217)
  %218 = alloca %ref_Test.Vec2, align 8
  %219 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %218, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %219, align 8
  %220 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %218, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %220, align 8
  %221 = alloca %ref_Test.Vec2, align 8
  %222 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %221, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %222, align 8
  %223 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %221, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %223, align 8
  %224 = load %ref_Test.Vec2, %ref_Test.Vec2* %221, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %224)
  %225 = alloca %ref_Test.Vec2, align 8
  %226 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %225, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %226, align 8
  %227 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %225, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %227, align 8
  %228 = alloca %ref_Test.Vec2, align 8
  %229 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %228, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %229, align 8
  %230 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %228, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %230, align 8
  %231 = load %ref_Test.Vec2, %ref_Test.Vec2* %228, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %231)
  %232 = alloca %ref_Test.Vec2, align 8
  %233 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %232, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %233, align 8
  %234 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %232, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %234, align 8
  %235 = alloca %ref_Test.Vec2, align 8
  %236 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %235, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %236, align 8
  %237 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %235, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %237, align 8
  %238 = load %ref_Test.Vec2, %ref_Test.Vec2* %235, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %238)
  %239 = alloca %ref_Test.Vec2, align 8
  %240 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %239, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %240, align 8
  %241 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %239, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %241, align 8
  %242 = alloca %ref_Test.Vec2, align 8
  %243 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %242, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %243, align 8
  %244 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %242, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %244, align 8
  %245 = load %ref_Test.Vec2, %ref_Test.Vec2* %242, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %245)
  %246 = alloca %ref_Test.Vec2, align 8
  %247 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %246, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %247, align 8
  %248 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %246, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %248, align 8
  %249 = alloca %ref_Test.Vec2, align 8
  %250 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %249, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %250, align 8
  %251 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %249, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %251, align 8
  %252 = load %ref_Test.Vec2, %ref_Test.Vec2* %249, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %252)
  %253 = alloca %ref_Test.Vec2, align 8
  %254 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %253, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %254, align 8
  %255 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %253, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %255, align 8
  %256 = alloca %ref_Test.Vec2, align 8
  %257 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %256, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %257, align 8
  %258 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %256, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %258, align 8
  %259 = load %ref_Test.Vec2, %ref_Test.Vec2* %256, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %259)
  %260 = alloca %ref_Test.Vec2, align 8
  %261 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %260, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %261, align 8
  %262 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %260, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %262, align 8
  %263 = alloca %ref_Test.Vec2, align 8
  %264 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %263, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %264, align 8
  %265 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %263, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %265, align 8
  %266 = load %ref_Test.Vec2, %ref_Test.Vec2* %263, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %266)
  %267 = alloca %ref_Test.Vec2, align 8
  %268 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %267, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %268, align 8
  %269 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %267, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %269, align 8
  %270 = alloca %ref_Test.Vec2, align 8
  %271 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %270, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %271, align 8
  %272 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %270, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %272, align 8
  %273 = load %ref_Test.Vec2, %ref_Test.Vec2* %270, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %273)
  %274 = alloca %ref_Test.Vec2, align 8
  %275 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %274, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %275, align 8
  %276 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %274, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %276, align 8
  %277 = alloca %ref_Test.Vec2, align 8
  %278 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %277, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %278, align 8
  %279 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %277, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %279, align 8
  %280 = load %ref_Test.Vec2, %ref_Test.Vec2* %277, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %280)
  %281 = alloca %ref_Test.Vec2, align 8
  %282 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %281, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %282, align 8
  %283 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %281, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %283, align 8
  %284 = alloca %ref_Test.Vec2, align 8
  %285 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %284, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %285, align 8
  %286 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %284, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %286, align 8
  %287 = load %ref_Test.Vec2, %ref_Test.Vec2* %284, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %287)
  %288 = alloca %ref_Test.Vec2, align 8
  %289 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %288, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %289, align 8
  %290 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %288, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %290, align 8
  %291 = alloca %ref_Test.Vec2, align 8
  %292 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %291, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %292, align 8
  %293 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %291, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %293, align 8
  %294 = load %ref_Test.Vec2, %ref_Test.Vec2* %291, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %294)
  %295 = alloca %ref_Test.Vec2, align 8
  %296 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %295, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %296, align 8
  %297 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %295, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %297, align 8
  %298 = alloca %ref_Test.Vec2, align 8
  %299 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %298, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %299, align 8
  %300 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %298, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %300, align 8
  %301 = load %ref_Test.Vec2, %ref_Test.Vec2* %298, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %301)
  %302 = alloca %ref_Test.Vec2, align 8
  %303 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %302, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %303, align 8
  %304 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %302, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %304, align 8
  %305 = alloca %ref_Test.Vec2, align 8
  %306 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %305, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %306, align 8
  %307 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %305, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %307, align 8
  %308 = load %ref_Test.Vec2, %ref_Test.Vec2* %305, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %308)
  %309 = alloca %ref_Test.Vec2, align 8
  %310 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %309, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %310, align 8
  %311 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %309, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %311, align 8
  %312 = alloca %ref_Test.Vec2, align 8
  %313 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %312, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %313, align 8
  %314 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %312, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %314, align 8
  %315 = load %ref_Test.Vec2, %ref_Test.Vec2* %312, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %315)
  %316 = alloca %ref_Test.Vec2, align 8
  %317 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %316, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %317, align 8
  %318 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %316, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %318, align 8
  %319 = alloca %ref_Test.Vec2, align 8
  %320 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %319, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %320, align 8
  %321 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %319, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %321, align 8
  %322 = load %ref_Test.Vec2, %ref_Test.Vec2* %319, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %322)
  %323 = alloca %ref_Test.Vec2, align 8
  %324 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %323, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %324, align 8
  %325 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %323, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %325, align 8
  %326 = alloca %ref_Test.Vec2, align 8
  %327 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %326, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %327, align 8
  %328 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %326, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %328, align 8
  %329 = load %ref_Test.Vec2, %ref_Test.Vec2* %326, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %329)
  %330 = alloca %ref_Test.Vec2, align 8
  %331 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %330, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %331, align 8
  %332 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %330, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %332, align 8
  %333 = alloca %ref_Test.Vec2, align 8
  %334 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %333, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %334, align 8
  %335 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %333, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %335, align 8
  %336 = load %ref_Test.Vec2, %ref_Test.Vec2* %333, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %336)
  %337 = alloca %ref_Test.Vec2, align 8
  %338 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %337, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %338, align 8
  %339 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %337, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %339, align 8
  %340 = alloca %ref_Test.Vec2, align 8
  %341 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %340, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %341, align 8
  %342 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %340, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %342, align 8
  %343 = load %ref_Test.Vec2, %ref_Test.Vec2* %340, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %343)
  %344 = alloca %ref_Test.Vec2, align 8
  %345 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %344, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %345, align 8
  %346 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %344, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %346, align 8
  %347 = alloca %ref_Test.Vec2, align 8
  %348 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %347, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %348, align 8
  %349 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %347, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %349, align 8
  %350 = load %ref_Test.Vec2, %ref_Test.Vec2* %347, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %350)
  %351 = alloca %ref_Test.Vec2, align 8
  %352 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %351, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %352, align 8
  %353 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %351, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %353, align 8
  %354 = alloca %ref_Test.Vec2, align 8
  %355 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %354, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %355, align 8
  %356 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %354, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %356, align 8
  %357 = load %ref_Test.Vec2, %ref_Test.Vec2* %354, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %357)
  %358 = alloca %ref_Test.Vec2, align 8
  %359 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %358, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %359, align 8
  %360 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %358, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %360, align 8
  %361 = alloca %ref_Test.Vec2, align 8
  %362 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %361, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %362, align 8
  %363 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %361, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %363, align 8
  %364 = load %ref_Test.Vec2, %ref_Test.Vec2* %361, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %364)
  %365 = alloca %ref_Test.Vec2, align 8
  %366 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %365, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %366, align 8
  %367 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %365, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %367, align 8
  %368 = alloca %ref_Test.Vec2, align 8
  %369 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %368, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %369, align 8
  %370 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %368, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %370, align 8
  %371 = load %ref_Test.Vec2, %ref_Test.Vec2* %368, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %371)
  %372 = alloca %ref_Test.Vec2, align 8
  %373 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %372, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %373, align 8
  %374 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %372, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %374, align 8
  %375 = alloca %ref_Test.Vec2, align 8
  %376 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %375, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %376, align 8
  %377 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %375, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %377, align 8
  %378 = load %ref_Test.Vec2, %ref_Test.Vec2* %375, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %378)
  %379 = alloca %ref_Test.Vec2, align 8
  %380 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %379, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %380, align 8
  %381 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %379, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %381, align 8
  %382 = alloca %ref_Test.Vec2, align 8
  %383 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %382, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %383, align 8
  %384 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %382, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %384, align 8
  %385 = load %ref_Test.Vec2, %ref_Test.Vec2* %382, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %385)
  %386 = alloca %ref_Test.Vec2, align 8
  %387 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %386, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %387, align 8
  %388 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %386, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %388, align 8
  %389 = alloca %ref_Test.Vec2, align 8
  %390 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %389, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %390, align 8
  %391 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %389, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %391, align 8
  %392 = load %ref_Test.Vec2, %ref_Test.Vec2* %389, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %392)
  %393 = alloca %ref_Test.Vec2, align 8
  %394 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %393, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %394, align 8
  %395 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %393, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %395, align 8
  %396 = alloca %ref_Test.Vec2, align 8
  %397 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %396, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %397, align 8
  %398 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %396, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %398, align 8
  %399 = load %ref_Test.Vec2, %ref_Test.Vec2* %396, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %399)
  %400 = alloca %ref_Test.Vec2, align 8
  %401 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %400, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %401, align 8
  %402 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %400, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %402, align 8
  %403 = alloca %ref_Test.Vec2, align 8
  %404 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %403, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %404, align 8
  %405 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %403, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %405, align 8
  %406 = load %ref_Test.Vec2, %ref_Test.Vec2* %403, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %406)
  %407 = alloca %ref_Test.Vec2, align 8
  %408 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %407, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %408, align 8
  %409 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %407, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %409, align 8
  %410 = alloca %ref_Test.Vec2, align 8
  %411 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %410, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %411, align 8
  %412 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %410, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %412, align 8
  %413 = load %ref_Test.Vec2, %ref_Test.Vec2* %410, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %413)
  %414 = alloca %ref_Test.Vec2, align 8
  %415 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %414, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %415, align 8
  %416 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %414, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %416, align 8
  %417 = alloca %ref_Test.Vec2, align 8
  %418 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %417, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %418, align 8
  %419 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %417, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %419, align 8
  %420 = load %ref_Test.Vec2, %ref_Test.Vec2* %417, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %420)
  %421 = alloca %ref_Test.Vec2, align 8
  %422 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %421, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %422, align 8
  %423 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %421, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %423, align 8
  %424 = alloca %ref_Test.Vec2, align 8
  %425 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %424, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %425, align 8
  %426 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %424, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %426, align 8
  %427 = load %ref_Test.Vec2, %ref_Test.Vec2* %424, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %427)
  %428 = alloca %ref_Test.Vec2, align 8
  %429 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %428, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %429, align 8
  %430 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %428, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %430, align 8
  %431 = alloca %ref_Test.Vec2, align 8
  %432 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %431, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %432, align 8
  %433 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %431, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %433, align 8
  %434 = load %ref_Test.Vec2, %ref_Test.Vec2* %431, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %434)
  %435 = alloca %ref_Test.Vec2, align 8
  %436 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %435, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %436, align 8
  %437 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %435, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %437, align 8
  %438 = alloca %ref_Test.Vec2, align 8
  %439 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %438, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %439, align 8
  %440 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %438, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %440, align 8
  %441 = load %ref_Test.Vec2, %ref_Test.Vec2* %438, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %441)
  %442 = alloca %ref_Test.Vec2, align 8
  %443 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %442, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %443, align 8
  %444 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %442, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %444, align 8
  %445 = alloca %ref_Test.Vec2, align 8
  %446 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %445, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %446, align 8
  %447 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %445, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %447, align 8
  %448 = load %ref_Test.Vec2, %ref_Test.Vec2* %445, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %448)
  %449 = alloca %ref_Test.Vec2, align 8
  %450 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %449, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %450, align 8
  %451 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %449, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %451, align 8
  %452 = alloca %ref_Test.Vec2, align 8
  %453 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %452, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %453, align 8
  %454 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %452, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %454, align 8
  %455 = load %ref_Test.Vec2, %ref_Test.Vec2* %452, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %455)
  %456 = alloca %ref_Test.Vec2, align 8
  %457 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %456, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %457, align 8
  %458 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %456, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %458, align 8
  %459 = alloca %ref_Test.Vec2, align 8
  %460 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %459, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %460, align 8
  %461 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %459, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %461, align 8
  %462 = load %ref_Test.Vec2, %ref_Test.Vec2* %459, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %462)
  %463 = alloca %ref_Test.Vec2, align 8
  %464 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %463, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %464, align 8
  %465 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %463, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %465, align 8
  %466 = alloca %ref_Test.Vec2, align 8
  %467 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %466, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %467, align 8
  %468 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %466, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %468, align 8
  %469 = load %ref_Test.Vec2, %ref_Test.Vec2* %466, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %469)
  %470 = alloca %ref_Test.Vec2, align 8
  %471 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %470, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %471, align 8
  %472 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %470, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %472, align 8
  %473 = alloca %ref_Test.Vec2, align 8
  %474 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %473, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %474, align 8
  %475 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %473, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %475, align 8
  %476 = load %ref_Test.Vec2, %ref_Test.Vec2* %473, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %476)
  %477 = alloca %ref_Test.Vec2, align 8
  %478 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %477, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %478, align 8
  %479 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %477, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %479, align 8
  %480 = alloca %ref_Test.Vec2, align 8
  %481 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %480, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %481, align 8
  %482 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %480, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %482, align 8
  %483 = load %ref_Test.Vec2, %ref_Test.Vec2* %480, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %483)
  %484 = alloca %ref_Test.Vec2, align 8
  %485 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %484, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %485, align 8
  %486 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %484, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %486, align 8
  %487 = alloca %ref_Test.Vec2, align 8
  %488 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %487, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %488, align 8
  %489 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %487, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %489, align 8
  %490 = load %ref_Test.Vec2, %ref_Test.Vec2* %487, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %490)
  %491 = alloca %ref_Test.Vec2, align 8
  %492 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %491, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %492, align 8
  %493 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %491, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %493, align 8
  %494 = alloca %ref_Test.Vec2, align 8
  %495 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %494, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %495, align 8
  %496 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %494, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %496, align 8
  %497 = load %ref_Test.Vec2, %ref_Test.Vec2* %494, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %497)
  %498 = alloca %ref_Test.Vec2, align 8
  %499 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %498, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %499, align 8
  %500 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %498, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %500, align 8
  %501 = alloca %ref_Test.Vec2, align 8
  %502 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %501, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %502, align 8
  %503 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %501, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %503, align 8
  %504 = load %ref_Test.Vec2, %ref_Test.Vec2* %501, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %504)
  %505 = alloca %ref_Test.Vec2, align 8
  %506 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %505, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %506, align 8
  %507 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %505, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %507, align 8
  %508 = alloca %ref_Test.Vec2, align 8
  %509 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %508, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %509, align 8
  %510 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %508, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %510, align 8
  %511 = load %ref_Test.Vec2, %ref_Test.Vec2* %508, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %511)
  %512 = alloca %ref_Test.Vec2, align 8
  %513 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %512, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %513, align 8
  %514 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %512, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %514, align 8
  %515 = alloca %ref_Test.Vec2, align 8
  %516 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %515, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %516, align 8
  %517 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %515, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %517, align 8
  %518 = load %ref_Test.Vec2, %ref_Test.Vec2* %515, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %518)
  %519 = alloca %ref_Test.Vec2, align 8
  %520 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %519, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %520, align 8
  %521 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %519, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %521, align 8
  %522 = alloca %ref_Test.Vec2, align 8
  %523 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %522, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %523, align 8
  %524 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %522, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %524, align 8
  %525 = load %ref_Test.Vec2, %ref_Test.Vec2* %522, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %525)
  %526 = alloca %ref_Test.Vec2, align 8
  %527 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %526, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %527, align 8
  %528 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %526, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %528, align 8
  %529 = alloca %ref_Test.Vec2, align 8
  %530 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %529, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %530, align 8
  %531 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %529, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %531, align 8
  %532 = load %ref_Test.Vec2, %ref_Test.Vec2* %529, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %532)
  %533 = alloca %ref_Test.Vec2, align 8
  %534 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %533, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %534, align 8
  %535 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %533, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %535, align 8
  %536 = alloca %ref_Test.Vec2, align 8
  %537 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %536, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %537, align 8
  %538 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %536, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %538, align 8
  %539 = load %ref_Test.Vec2, %ref_Test.Vec2* %536, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %539)
  %540 = alloca %ref_Test.Vec2, align 8
  %541 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %540, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %541, align 8
  %542 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %540, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %542, align 8
  %543 = alloca %ref_Test.Vec2, align 8
  %544 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %543, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %544, align 8
  %545 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %543, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %545, align 8
  %546 = load %ref_Test.Vec2, %ref_Test.Vec2* %543, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %546)
  %547 = alloca %ref_Test.Vec2, align 8
  %548 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %547, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %548, align 8
  %549 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %547, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %549, align 8
  %550 = alloca %ref_Test.Vec2, align 8
  %551 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %550, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %551, align 8
  %552 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %550, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %552, align 8
  %553 = load %ref_Test.Vec2, %ref_Test.Vec2* %550, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %553)
  %554 = alloca %ref_Test.Vec2, align 8
  %555 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %554, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %555, align 8
  %556 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %554, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %556, align 8
  %557 = alloca %ref_Test.Vec2, align 8
  %558 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %557, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %558, align 8
  %559 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %557, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %559, align 8
  %560 = load %ref_Test.Vec2, %ref_Test.Vec2* %557, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %560)
  %561 = alloca %ref_Test.Vec2, align 8
  %562 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %561, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %562, align 8
  %563 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %561, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %563, align 8
  %564 = alloca %ref_Test.Vec2, align 8
  %565 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %564, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %565, align 8
  %566 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %564, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %566, align 8
  %567 = load %ref_Test.Vec2, %ref_Test.Vec2* %564, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %567)
  %568 = alloca %ref_Test.Vec2, align 8
  %569 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %568, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %569, align 8
  %570 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %568, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %570, align 8
  %571 = alloca %ref_Test.Vec2, align 8
  %572 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %571, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %572, align 8
  %573 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %571, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %573, align 8
  %574 = load %ref_Test.Vec2, %ref_Test.Vec2* %571, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %574)
  %575 = alloca %ref_Test.Vec2, align 8
  %576 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %575, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %576, align 8
  %577 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %575, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %577, align 8
  %578 = alloca %ref_Test.Vec2, align 8
  %579 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %578, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %579, align 8
  %580 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %578, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %580, align 8
  %581 = load %ref_Test.Vec2, %ref_Test.Vec2* %578, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %581)
  %582 = alloca %ref_Test.Vec2, align 8
  %583 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %582, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %583, align 8
  %584 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %582, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %584, align 8
  %585 = alloca %ref_Test.Vec2, align 8
  %586 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %585, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %586, align 8
  %587 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %585, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %587, align 8
  %588 = load %ref_Test.Vec2, %ref_Test.Vec2* %585, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %588)
  %589 = alloca %ref_Test.Vec2, align 8
  %590 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %589, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %590, align 8
  %591 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %589, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %591, align 8
  %592 = alloca %ref_Test.Vec2, align 8
  %593 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %592, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %593, align 8
  %594 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %592, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %594, align 8
  %595 = load %ref_Test.Vec2, %ref_Test.Vec2* %592, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %595)
  %596 = alloca %ref_Test.Vec2, align 8
  %597 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %596, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %597, align 8
  %598 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %596, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %598, align 8
  %599 = alloca %ref_Test.Vec2, align 8
  %600 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %599, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %600, align 8
  %601 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %599, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %601, align 8
  %602 = load %ref_Test.Vec2, %ref_Test.Vec2* %599, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %602)
  %603 = alloca %ref_Test.Vec2, align 8
  %604 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %603, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %604, align 8
  %605 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %603, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %605, align 8
  %606 = alloca %ref_Test.Vec2, align 8
  %607 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %606, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %607, align 8
  %608 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %606, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %608, align 8
  %609 = load %ref_Test.Vec2, %ref_Test.Vec2* %606, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %609)
  %610 = alloca %ref_Test.Vec2, align 8
  %611 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %610, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %611, align 8
  %612 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %610, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %612, align 8
  %613 = alloca %ref_Test.Vec2, align 8
  %614 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %613, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %614, align 8
  %615 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %613, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %615, align 8
  %616 = load %ref_Test.Vec2, %ref_Test.Vec2* %613, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %616)
  %617 = alloca %ref_Test.Vec2, align 8
  %618 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %617, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %618, align 8
  %619 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %617, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %619, align 8
  %620 = alloca %ref_Test.Vec2, align 8
  %621 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %620, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %621, align 8
  %622 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %620, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %622, align 8
  %623 = load %ref_Test.Vec2, %ref_Test.Vec2* %620, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %623)
  %624 = alloca %ref_Test.Vec2, align 8
  %625 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %624, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %625, align 8
  %626 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %624, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %626, align 8
  %627 = alloca %ref_Test.Vec2, align 8
  %628 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %627, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %628, align 8
  %629 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %627, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %629, align 8
  %630 = load %ref_Test.Vec2, %ref_Test.Vec2* %627, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %630)
  %631 = alloca %ref_Test.Vec2, align 8
  %632 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %631, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %632, align 8
  %633 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %631, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %633, align 8
  %634 = alloca %ref_Test.Vec2, align 8
  %635 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %634, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %635, align 8
  %636 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %634, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %636, align 8
  %637 = load %ref_Test.Vec2, %ref_Test.Vec2* %634, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %637)
  %638 = alloca %ref_Test.Vec2, align 8
  %639 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %638, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %639, align 8
  %640 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %638, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %640, align 8
  %641 = alloca %ref_Test.Vec2, align 8
  %642 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %641, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %642, align 8
  %643 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %641, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %643, align 8
  %644 = load %ref_Test.Vec2, %ref_Test.Vec2* %641, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %644)
  %645 = alloca %ref_Test.Vec2, align 8
  %646 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %645, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %646, align 8
  %647 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %645, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %647, align 8
  %648 = alloca %ref_Test.Vec2, align 8
  %649 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %648, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %649, align 8
  %650 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %648, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %650, align 8
  %651 = load %ref_Test.Vec2, %ref_Test.Vec2* %648, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %651)
  %652 = alloca %ref_Test.Vec2, align 8
  %653 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %652, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %653, align 8
  %654 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %652, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %654, align 8
  %655 = alloca %ref_Test.Vec2, align 8
  %656 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %655, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %656, align 8
  %657 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %655, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %657, align 8
  %658 = load %ref_Test.Vec2, %ref_Test.Vec2* %655, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %658)
  %659 = alloca %ref_Test.Vec2, align 8
  %660 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %659, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %660, align 8
  %661 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %659, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %661, align 8
  %662 = alloca %ref_Test.Vec2, align 8
  %663 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %662, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %663, align 8
  %664 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %662, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %664, align 8
  %665 = load %ref_Test.Vec2, %ref_Test.Vec2* %662, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %665)
  %666 = alloca %ref_Test.Vec2, align 8
  %667 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %666, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %667, align 8
  %668 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %666, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %668, align 8
  %669 = alloca %ref_Test.Vec2, align 8
  %670 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %669, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %670, align 8
  %671 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %669, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %671, align 8
  %672 = load %ref_Test.Vec2, %ref_Test.Vec2* %669, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %672)
  %673 = alloca %ref_Test.Vec2, align 8
  %674 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %673, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %674, align 8
  %675 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %673, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %675, align 8
  %676 = alloca %ref_Test.Vec2, align 8
  %677 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %676, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %677, align 8
  %678 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %676, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %678, align 8
  %679 = load %ref_Test.Vec2, %ref_Test.Vec2* %676, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %679)
  %680 = alloca %ref_Test.Vec2, align 8
  %681 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %680, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %681, align 8
  %682 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %680, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %682, align 8
  %683 = alloca %ref_Test.Vec2, align 8
  %684 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %683, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %684, align 8
  %685 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %683, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %685, align 8
  %686 = load %ref_Test.Vec2, %ref_Test.Vec2* %683, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %686)
  %687 = alloca %ref_Test.Vec2, align 8
  %688 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %687, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %688, align 8
  %689 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %687, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %689, align 8
  %690 = alloca %ref_Test.Vec2, align 8
  %691 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %690, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %691, align 8
  %692 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %690, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %692, align 8
  %693 = load %ref_Test.Vec2, %ref_Test.Vec2* %690, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %693)
  %694 = alloca %ref_Test.Vec2, align 8
  %695 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %694, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %695, align 8
  %696 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %694, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %696, align 8
  %697 = alloca %ref_Test.Vec2, align 8
  %698 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %697, i32 0, i32 0
  store %struct_Test_Vec2* %vec, %struct_Test_Vec2** %698, align 8
  %699 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %697, i32 0, i32 1
  store %__allocator_vtable* null, %__allocator_vtable** %699, align 8
  %700 = load %ref_Test.Vec2, %ref_Test.Vec2* %697, align 8
  call void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %700)
  ret i8 0
}

define void @function_Test_toString_ref_Test.Vec2_void(%ref_Test.Vec2 %0) {
  %2 = alloca %ref_Test.Vec2, align 8
  store %ref_Test.Vec2 %0, %ref_Test.Vec2* %2, align 8
  %3 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %2, i32 0, i32 1
  %4 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %2, i32 0, i32 0
  %5 = load %struct_Test_Vec2*, %struct_Test_Vec2** %4, align 8
  %6 = getelementptr inbounds %struct_Test_Vec2, %struct_Test_Vec2* %5, i32 0, i32 0
  %7 = getelementptr inbounds %ref_Test.Vec2, %ref_Test.Vec2* %2, i32 0, i32 0
  %8 = load %struct_Test_Vec2*, %struct_Test_Vec2** %7, align 8
  %9 = getelementptr inbounds %struct_Test_Vec2, %struct_Test_Vec2* %8, i32 0, i32 1
  %10 = load i8, i8* %6, align 1
  %11 = load i8, i8* %9, align 1
  %12 = call i32 (i8*, ...) @printf(i8* getelementptr inbounds ([9 x i8], [9 x i8]* @0, i64 0, i64 0), i8 %10, i8 %11)
  ret void
}
