# Leaf
**This repository contains an incomplete, legacy version of Leaf's compiler, which is currently being re-written.
This is just a messy proof of concept and should be treated as such. This repo will soon be archived and replaced by a new, proper version of the compiler.**

## Overview
Leaf is a performance-oriented compiled programming language.
The intended audience is the performance-minded developer who values readability, simplicity and fast development iteration.

## Design goals
- Leveraging the LLVM and CLR infrastructures

- High performance execution
  - No GC or ref counting overhead
  - Only pay for what you use
  - Minimal runtime

- Control over memory
  - Support for custom allocators
  - Enhanced control over stack memory

- Low-friction interop with C and C++
  - Statically or dynamically link to ordinary C/C++ libraries
  - Support for C/C++ structure layouts and calling conventions

- Low-friction interop with C# and the .Net platform.
  - Support for CLR assebly generation.

If you'd like to know more about Leaf, please refer to the [Wiki](https://github.com/MaximumOverflow/Leaf/wiki).
