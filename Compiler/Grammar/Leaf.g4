grammar Leaf;

WS: [\n\t ] -> skip;
Escape: '\\' .;
Comment: ('//' .*? '\n') -> skip;
MultiLineComment: ('/*' .*? '*/') -> skip;


Def: 'def';
Ref: 'ref';
Var: 'var';
Let: 'let';
Pub: 'pub';
Free: 'free';
Static: 'static';
TypeOf: 'typeof';
SizeOf: 'sizeof';
Return: 'return';

Struct: 'struct';
Operator: 'operator';
Allocator: 'allocator';

StaticAccessor: '::';
DynamicAccessor: '.';

In: 'in';
Out: 'out';

Add: '+';
Sub: '-';
Mul: '*';
Div: '/';
Mod: '%';
As:  'as';

Eq: '==';
Neq: '!=';

If: 'if';
For: 'for';
Else: 'else';
While: 'while';

Range: '..';
Group: '...';


True: 'true';
False: 'false';
Integer: '-'? [0-9]+;
FloatingPoint: '-'? Integer? '.' Integer;

Char: '\''(Escape | ~[\\']) '\'';
String: '"' (Escape | ~[\\"])* '"';
CString: '&' '"' (Escape | ~[\\"])* '"';

Id: [a-zA-Z_][a-zA-Z0-9_]*;
namespace: Id '.' Id;
operator_id: Id | Add | Sub | Mul | Div | Mod | As | DynamicAccessor;

//#### Types ####
type
	: (ns=Id '.')? name=Id generic_impl_list?
	| type ptr='*'
	| Ref type
	| '(' nested=type ')'
	| '(' tuple=type (',' type)*')'
	| '(' (type (',' type)*) ')' '->' ret=type
	| struct | allocator | attribute
	//Reflection
	| TypeOf '(' value ')'
	| type StaticAccessor property=Id
	| type StaticAccessor funcCall=Id generic_impl_list? '(' (value (',' value)*)? ')'
	;

type_member: Pub? Static? Id ':' type;
struct: 'struct' '{' (type_member ';')* '}';

attribute: 'attribute' '{' (type_member ';')* '}';
attribute_add: '@' (ns=Id '.')? name=Id generic_impl_list? ('(' value (',' value)* ')')?;

allocator: 'allocator' '{' (type_member ';')* '}';


//#### Definitions ####
def_type: Def Id generic_def_list? ':' (struct);
def_func: Def Id generic_def_list? ':' (function_decl | function_impl);
def_operator: Def 'operator' operator_id generic_def_list? ':' function_impl;

def: attribute_add* (def_type | def_func | def_operator);


//#### Functions ####
parameter_def: Id ':' mut='mut'? type;
function_decl: '(' (parameter_def (',' parameter_def)*)? ')' '->' type ';';
function_impl: '(' (parameter_def (',' parameter_def)*)? ')' ('->' type)? function_scope;

function_call: value '(' (value (',' value)*)? ')';

function_scope
	: '=>' value
	| '=>' statement
	| '{' statement* '}'
	;


//#### Values ####
integer: Integer;
floating_point: FloatingPoint;

value
	: Id generic_impl_list?
	| value (nested='.' value)
	| '(' par=value ')'
	| call=value '(' (value (',' value)*)? ')'
	//Constants
	| True | False
	| integer | floating_point 
	| Char | String | CString
	//Operators
	| value (Mul | Div | Mod) value
	| value (Add | Sub) value
	| value (Eq | Neq) value
	//References
	| addrOf='&' value
	| deref ='*' value
	| Ref value
	//Constructs
	| initialization_list
	| value Range value
	//Reflection
	| SizeOf '(' type ')'
	| type StaticAccessor property=Id
    | type StaticAccessor funcCall=Id generic_impl_list? '(' (value (',' value)*)? ')'
	;
	
initialization_list: type? '{' (Id '=' value (',' Id '=' value)*)? '}';

//#### Statements ####
scope: '{' statement* '}';
conditional_scope
	: '=>' statement 
	| '{' statement* '}';

var_def_t: Var Id ':' t=type (':' alloc=type)?;
var_def_v: (Var | Let) Ref? Id (':' t=type)? '=' value (':' alloc=type)?;

var_ass: value '=' value;
var_def: var_def_v | var_def_t;

if: If (value | var_def) conditional_scope
   (Else (if | conditional_scope))?;

while: While value conditional_scope;

for: For (('['name=Id ',' index=Id']') | name=Id) In value conditional_scope;

return: Return (cond='?'? value)?;

free: Free value;

statement
	: (var_def | var_ass) ';'
	| function_call ';'
	| if | while | for
	| return ';'
	| free ';'
	;


//#### Lists ####
generic_def_list:  '<' Id (',' Id)* '>';
generic_impl_list: '<' type (',' type)* '>';


//#### Entry Point ####
ns_import: 'import' namespace ('as' alias=Id);
entry_point: ns_import* def*;