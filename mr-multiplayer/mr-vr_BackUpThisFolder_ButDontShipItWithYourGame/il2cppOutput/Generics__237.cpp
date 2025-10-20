#include "pch-cpp.hpp"





template <typename T1, typename T2>
struct VirtualActionInvoker2
{
	typedef void (*Action)(void*, T1, T2, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};
template <typename T1>
struct InvokerActionInvoker1;
template <typename T1>
struct InvokerActionInvoker1<T1*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, params[0]);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2;
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		void* params[2] = { p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, params[1]);
	}
};
template <typename R, typename T1>
struct InvokerFuncInvoker1;
template <typename R, typename T1>
struct InvokerFuncInvoker1<R, T1*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		R ret;
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};

struct Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04;
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
struct Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821;
struct Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00;
struct Func_2_t213311159653563BDCC21CC060B449705C96791F;
struct Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B;
struct Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0;
struct Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52;
struct Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8;
struct Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF;
struct Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041;
struct Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218;
struct Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E;
struct _SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414;
struct _SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7;
struct _SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25;
struct _Take_tAC9861ABFEB04204FDE9EBE25D87AFDB0BFAB794;
struct _Take_t08AD65AC2F84512F4F4EC1985D42D832B58FDCD8;
struct _Where_tD6DDDF0FBCC547F4D1026D133796D380D1E4F5F9;
struct _Where_t07B688566F1CE2EC195CF2412082E2BF789D59B5;
struct _Where_t9D34EE9F2F3CBD5E31620008200996D18315AD88;
struct _WhereSelect_tCDFD2355712F51F0A6954F27E79B53E608FAC324;
struct _WhereSelect_tD4006D864F2881A24658C4FB88E5D6D0D1C2DF1D;
struct _WhereSelect_tA9DEB5E540178B4BE04D7FCBEB5DFC2AE5F1A98E;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct Exception_t;
struct FrameProvider_tB15460EB6BB3CA843538EE4B788E0579699D2F26;
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5;
struct IThreadPoolWorkItem_t9A3A463A5670CDDDC4BFE39B355E199D72F1AAD0;
struct MethodInfo_t;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E;
struct String_t;
struct SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0;
struct TimeProvider_t37846A32FB1F52B8572CDF43ECE9852159346249;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ThreadPool_tCF41DF106471C552043F4C43B9881CA49A5D76CF_il2cpp_TypeInfo_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52  : public RuntimeObject
{
};
struct Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8  : public RuntimeObject
{
};
struct ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 
{
	Exception_t* ___U3CExceptionU3Ek__BackingField;
};
struct Result_t1EEFFD204479E80A86CE4735CA81396E04B55412_marshaled_pinvoke
{
	Exception_t_marshaled_pinvoke* ___U3CExceptionU3Ek__BackingField;
};
struct Result_t1EEFFD204479E80A86CE4735CA81396E04B55412_marshaled_com
{
	Exception_t_marshaled_com* ___U3CExceptionU3Ek__BackingField;
};
struct SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 
{
	RuntimeObject* ___current;
};
struct SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8_marshaled_pinvoke
{
	RuntimeObject* ___current;
};
struct SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8_marshaled_com
{
	RuntimeObject* ___current;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF  : public RuntimeObject
{
	SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 ___SourceSubscription;
	int32_t ___calledOnCompleted;
	int32_t ___disposed;
};
struct Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041  : public RuntimeObject
{
	SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 ___SourceSubscription;
	int32_t ___calledOnCompleted;
	int32_t ___disposed;
};
struct Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218  : public RuntimeObject
{
	SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 ___SourceSubscription;
	int32_t ___calledOnCompleted;
	int32_t ___disposed;
};
struct Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E  : public RuntimeObject
{
	SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 ___SourceSubscription;
	int32_t ___calledOnCompleted;
	int32_t ___disposed;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Int32Enum_tCBAC8BA2BFF3A845FA599F303093BBBA374B6F0C 
{
	int32_t ___value__;
};
struct SynchronizationContextProperties_t5ED82C778B4C396AD94A93CFBEF00022BDECF058 
{
	int32_t ___value__;
};
struct _SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414  : public Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218
{
	Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___observer;
	Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52* ___source;
	SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 ___disposable;
};
struct _SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7  : public Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E
{
	Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___observer;
	Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* ___source;
	SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0* ___synchronizationContext;
	SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 ___disposable;
};
struct _SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25  : public Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E
{
	Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___observer;
	Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* ___source;
	SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8 ___disposable;
};
struct _Take_tAC9861ABFEB04204FDE9EBE25D87AFDB0BFAB794  : public Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218
{
	Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___U3CobserverU3EP;
	int32_t ___remaining;
};
struct _Take_t08AD65AC2F84512F4F4EC1985D42D832B58FDCD8  : public Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E
{
	Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___U3CobserverU3EP;
	int32_t ___remaining;
};
struct _Where_tD6DDDF0FBCC547F4D1026D133796D380D1E4F5F9  : public Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041
{
	Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* ___U3CobserverU3EP;
	Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* ___U3CpredicateU3EP;
};
struct _Where_t07B688566F1CE2EC195CF2412082E2BF789D59B5  : public Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218
{
	Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___U3CobserverU3EP;
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___U3CpredicateU3EP;
};
struct _Where_t9D34EE9F2F3CBD5E31620008200996D18315AD88  : public Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E
{
	Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___U3CobserverU3EP;
	Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___U3CpredicateU3EP;
};
struct _WhereSelect_tCDFD2355712F51F0A6954F27E79B53E608FAC324  : public Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041
{
	Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* ___U3CobserverU3EP;
	Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* ___U3CselectorU3EP;
	Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* ___U3CpredicateU3EP;
};
struct _WhereSelect_tD4006D864F2881A24658C4FB88E5D6D0D1C2DF1D  : public Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218
{
	Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* ___U3CobserverU3EP;
	Func_2_t213311159653563BDCC21CC060B449705C96791F* ___U3CselectorU3EP;
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___U3CpredicateU3EP;
};
struct _WhereSelect_tA9DEB5E540178B4BE04D7FCBEB5DFC2AE5F1A98E  : public Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E
{
	Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___U3CobserverU3EP;
	Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* ___U3CselectorU3EP;
	Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___U3CpredicateU3EP;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0  : public RuntimeObject
{
	int32_t ____props;
};
struct Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04  : public MulticastDelegate_t
{
};
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87  : public MulticastDelegate_t
{
};
struct Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821  : public MulticastDelegate_t
{
};
struct Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00  : public MulticastDelegate_t
{
};
struct Func_2_t213311159653563BDCC21CC060B449705C96791F  : public MulticastDelegate_t
{
};
struct Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B  : public MulticastDelegate_t
{
};
struct Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0  : public MulticastDelegate_t
{
};
struct SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E  : public MulticastDelegate_t
{
};
struct ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_StaticFields
{
	TimeProvider_t37846A32FB1F52B8572CDF43ECE9852159346249* ___defaultTimeProvider;
	FrameProvider_tB15460EB6BB3CA843538EE4B788E0579699D2F26* ___defaultFrameProvider;
	Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04* ___unhandledException;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct Exception_t_StaticFields
{
	RuntimeObject* ___s_EDILock;
};
struct _SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7_StaticFields
{
	SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E* ___postCallback;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1__ctor_m35F6F15C7CE59E0B904DED1A3ED7EC9B917E4046_gshared (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Observable_1_Subscribe_mC5D39E813BC6A9EDA9D1DF950A9F3A27AA8DDDA1_gshared (Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52* __this, Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___0_observer, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_Dispose_mB2BA07B3BA9DE00BAED359768AA04C9C80488798_gshared (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnNext_mD7BB2434F471B0F546CEB286FBEF1D4D77FB0160_gshared (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnErrorResume_mD83E7C18B8617CE8185159FD248A8B43F441F18C_gshared (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, Exception_t* ___0_error, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnCompleted_mFC826873BB8CBFFF95D05B873EFEFC0CBC2EDAC0_gshared (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ObserverExtensions_OnCompleted_TisRuntimeObject_mB6C7AA41E77FB2A0CC3902D7CC5BF427214588AD_gshared (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___0_observer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1__ctor_mA04A589605CB06CEA6F1D6DC47272145ADEC30F6_gshared (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m095D2006A2DDB336987862DC15A7EFAED53E08EC_gshared_inline (Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* __this, int32_t ___0_arg, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnNext_m6C50488E4BC6D037E00920D35ABA85213E74B790_gshared (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnErrorResume_mDF6B7FABE356230FF4EC32E78828650B4FD45CA2_gshared (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, Exception_t* ___0_error, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnCompleted_m26C8A1108383678F346278A974F9A7DAC3CC2A0A_gshared (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_gshared_inline (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* __this, RuntimeObject* ___0_arg, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnNext_m95727C008980996D1FA13041D3A987000792B120_gshared (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* __this, bool ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnErrorResume_m092497C454071B749B9E18F43D74BAB28A146FA2_gshared (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* __this, Exception_t* ___0_error, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Observer_1_OnCompleted_m834BA0F85254B3C28E6A18FE22CA926AF3B268DB_gshared (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Func_2_Invoke_m1FDB82A936AD6A68F455DE792FD9454CE1A4FC9F_gshared_inline (Func_2_t213311159653563BDCC21CC060B449705C96791F* __this, RuntimeObject* ___0_arg, const RuntimeMethod* method) ;

inline void Observer_1__ctor_m35F6F15C7CE59E0B904DED1A3ED7EC9B917E4046 (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*, const RuntimeMethod*))Observer_1__ctor_m35F6F15C7CE59E0B904DED1A3ED7EC9B917E4046_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ThreadPool_UnsafeQueueUserWorkItem_m084CD08E42EC545837C765F8FA2ACEBCD610DDD9 (RuntimeObject* ___0_callBack, bool ___1_preferLocal, const RuntimeMethod* method) ;
inline RuntimeObject* Observable_1_Subscribe_mC5D39E813BC6A9EDA9D1DF950A9F3A27AA8DDDA1 (Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52* __this, Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___0_observer, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52*, Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*, const RuntimeMethod*))Observable_1_Subscribe_mC5D39E813BC6A9EDA9D1DF950A9F3A27AA8DDDA1_gshared)(__this, ___0_observer, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SingleAssignmentDisposableCore_set_Disposable_m512607DE32E18CFB7D692A8EE8C1BFE7DD2BB510 (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04* ObservableSystem_GetUnhandledExceptionHandler_mABD5FA2181B4365ED37D6813295F874BFEC6A77F_inline (const RuntimeMethod* method) ;
inline void Action_1_Invoke_m43B5C4C0F292CE3E07CB03B46D8F960ACF7D6A58_inline (Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04* __this, Exception_t* ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04*, Exception_t*, const RuntimeMethod*))Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline)(__this, ___0_obj, method);
}
inline void Observer_1_Dispose_mB2BA07B3BA9DE00BAED359768AA04C9C80488798 (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*, const RuntimeMethod*))Observer_1_Dispose_mB2BA07B3BA9DE00BAED359768AA04C9C80488798_gshared)(__this, method);
}
inline void Observer_1_OnNext_mD7BB2434F471B0F546CEB286FBEF1D4D77FB0160 (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, RuntimeObject* ___0_value, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*, RuntimeObject*, const RuntimeMethod*))Observer_1_OnNext_mD7BB2434F471B0F546CEB286FBEF1D4D77FB0160_gshared)(__this, ___0_value, method);
}
inline void Observer_1_OnErrorResume_mD83E7C18B8617CE8185159FD248A8B43F441F18C (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, Exception_t* ___0_error, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*, Exception_t*, const RuntimeMethod*))Observer_1_OnErrorResume_mD83E7C18B8617CE8185159FD248A8B43F441F18C_gshared)(__this, ___0_error, method);
}
inline void Observer_1_OnCompleted_mFC826873BB8CBFFF95D05B873EFEFC0CBC2EDAC0 (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))Observer_1_OnCompleted_mFC826873BB8CBFFF95D05B873EFEFC0CBC2EDAC0_gshared)(__this, ___0_result, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SingleAssignmentDisposableCore_Dispose_m8FFF38AD98886810D7F28EF2C248F3DFF37513F0 (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SendOrPostCallback__ctor_mE6F9D9606A00C3C18AEA057422ECF4106C80DA37 (SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
inline void ObserverExtensions_OnCompleted_TisRuntimeObject_mB6C7AA41E77FB2A0CC3902D7CC5BF427214588AD (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___0_observer, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*, const RuntimeMethod*))ObserverExtensions_OnCompleted_TisRuntimeObject_mB6C7AA41E77FB2A0CC3902D7CC5BF427214588AD_gshared)(___0_observer, method);
}
inline void Observer_1__ctor_mA04A589605CB06CEA6F1D6DC47272145ADEC30F6 (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041*, const RuntimeMethod*))Observer_1__ctor_mA04A589605CB06CEA6F1D6DC47272145ADEC30F6_gshared)(__this, method);
}
inline bool Func_2_Invoke_m095D2006A2DDB336987862DC15A7EFAED53E08EC_inline (Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* __this, int32_t ___0_arg, const RuntimeMethod* method)
{
	return ((  bool (*) (Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821*, int32_t, const RuntimeMethod*))Func_2_Invoke_m095D2006A2DDB336987862DC15A7EFAED53E08EC_gshared_inline)(__this, ___0_arg, method);
}
inline void Observer_1_OnNext_m6C50488E4BC6D037E00920D35ABA85213E74B790 (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, int32_t ___0_value, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041*, int32_t, const RuntimeMethod*))Observer_1_OnNext_m6C50488E4BC6D037E00920D35ABA85213E74B790_gshared)(__this, ___0_value, method);
}
inline void Observer_1_OnErrorResume_mDF6B7FABE356230FF4EC32E78828650B4FD45CA2 (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, Exception_t* ___0_error, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041*, Exception_t*, const RuntimeMethod*))Observer_1_OnErrorResume_mDF6B7FABE356230FF4EC32E78828650B4FD45CA2_gshared)(__this, ___0_error, method);
}
inline void Observer_1_OnCompleted_m26C8A1108383678F346278A974F9A7DAC3CC2A0A (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))Observer_1_OnCompleted_m26C8A1108383678F346278A974F9A7DAC3CC2A0A_gshared)(__this, ___0_result, method);
}
inline bool Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* __this, RuntimeObject* ___0_arg, const RuntimeMethod* method)
{
	return ((  bool (*) (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, RuntimeObject*, const RuntimeMethod*))Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_gshared_inline)(__this, ___0_arg, method);
}
inline void Observer_1_OnNext_m95727C008980996D1FA13041D3A987000792B120 (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* __this, bool ___0_value, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF*, bool, const RuntimeMethod*))Observer_1_OnNext_m95727C008980996D1FA13041D3A987000792B120_gshared)(__this, ___0_value, method);
}
inline void Observer_1_OnErrorResume_m092497C454071B749B9E18F43D74BAB28A146FA2 (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* __this, Exception_t* ___0_error, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF*, Exception_t*, const RuntimeMethod*))Observer_1_OnErrorResume_m092497C454071B749B9E18F43D74BAB28A146FA2_gshared)(__this, ___0_error, method);
}
inline void Observer_1_OnCompleted_m834BA0F85254B3C28E6A18FE22CA926AF3B268DB (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method)
{
	((  void (*) (Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))Observer_1_OnCompleted_m834BA0F85254B3C28E6A18FE22CA926AF3B268DB_gshared)(__this, ___0_result, method);
}
inline int32_t Func_2_Invoke_m1FDB82A936AD6A68F455DE792FD9454CE1A4FC9F_inline (Func_2_t213311159653563BDCC21CC060B449705C96791F* __this, RuntimeObject* ___0_arg, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Func_2_t213311159653563BDCC21CC060B449705C96791F*, RuntimeObject*, const RuntimeMethod*))Func_2_Invoke_m1FDB82A936AD6A68F455DE792FD9454CE1A4FC9F_gshared_inline)(__this, ___0_arg, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108504
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn__ctor_m5ACBDA0BAC90B4920B96EEDE6DAEC53F8D7DCBEB_gshared (_SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414* __this, Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___0_observer, Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52* ___1_source, const RuntimeMethod* method) 
{
	{
		Observer_1__ctor_m35F6F15C7CE59E0B904DED1A3ED7EC9B917E4046((Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = ___0_observer;
		__this->___observer = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___observer), (void*)L_0);
		Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52* L_1 = ___1_source;
		__this->___source = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source), (void*)L_1);
		return;
	}
}
// Method Definition Index: 108505
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* _SubscribeOn_Run_mF9D9A27B7B7BA7A687151728BC33456E20E3C0B6_gshared (_SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ThreadPool_tCF41DF106471C552043F4C43B9881CA49A5D76CF_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(ThreadPool_tCF41DF106471C552043F4C43B9881CA49A5D76CF_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = ThreadPool_UnsafeQueueUserWorkItem_m084CD08E42EC545837C765F8FA2ACEBCD610DDD9((RuntimeObject*)__this, (bool)0, NULL);
		return (RuntimeObject*)__this;
	}
}
// Method Definition Index: 108506
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_Execute_m083BEE1FE877D529276A5ACAE289BD4D16A00046_gshared (_SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414* __this, const RuntimeMethod* method) 
{
	Exception_t* V_0 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	try
	{
		SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* L_0 = (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8*)(&__this->___disposable);
		Observable_1_tF80C7CA91331E4ED991D76CE9AD242688ABB0B52* L_1 = __this->___source;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = Observable_1_Subscribe_mC5D39E813BC6A9EDA9D1DF950A9F3A27AA8DDDA1(L_1, (Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		SingleAssignmentDisposableCore_set_Disposable_m512607DE32E18CFB7D692A8EE8C1BFE7DD2BB510(L_0, L_2, NULL);
		goto IL_002d;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0019;
		}
		throw e;
	}

CATCH_0019:
	{
		Exception_t* L_3 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));;
		V_0 = L_3;
		il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_il2cpp_TypeInfo_var)));
		Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04* L_4;
		L_4 = ObservableSystem_GetUnhandledExceptionHandler_mABD5FA2181B4365ED37D6813295F874BFEC6A77F_inline(NULL);
		Exception_t* L_5 = V_0;
		NullCheck(L_4);
		Action_1_Invoke_m43B5C4C0F292CE3E07CB03B46D8F960ACF7D6A58_inline(L_4, L_5, NULL);
		NullCheck((Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*)__this);
		Observer_1_Dispose_mB2BA07B3BA9DE00BAED359768AA04C9C80488798((Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002d;
	}

IL_002d:
	{
		return;
	}
}
// Method Definition Index: 108507
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnNextCore_m99CFFFE388366517F3CA16D57154F556E98208FB_gshared (_SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = __this->___observer;
		RuntimeObject* L_1 = ___0_value;
		NullCheck(L_0);
		Observer_1_OnNext_mD7BB2434F471B0F546CEB286FBEF1D4D77FB0160(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
// Method Definition Index: 108508
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnErrorResumeCore_m84C35BF71AB756B75E0CE01E37BB77AE90CE9E73_gshared (_SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = __this->___observer;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		Observer_1_OnErrorResume_mD83E7C18B8617CE8185159FD248A8B43F441F18C(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// Method Definition Index: 108509
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnCompletedCore_m55055D7D2D94AE704964044E2FF054426D9BF96A_gshared (_SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = __this->___observer;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		Observer_1_OnCompleted_mFC826873BB8CBFFF95D05B873EFEFC0CBC2EDAC0(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		return;
	}
}
// Method Definition Index: 108510
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_DisposeCore_m7AFB7A252823B43371A8DDA0F17216D267B07E3A_gshared (_SubscribeOn_t359E2D14D2A8C5BE72EAFE6D149D9C1371822414* __this, const RuntimeMethod* method) 
{
	{
		SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* L_0 = (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8*)(&__this->___disposable);
		SingleAssignmentDisposableCore_Dispose_m8FFF38AD98886810D7F28EF2C248F3DFF37513F0(L_0, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108494
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn__ctor_m26D0C4D38A5820331E056CCCD1D44E05EFF3F9B4_gshared (_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* __this, Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___0_observer, Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* ___1_source, SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0* ___2_synchronizationContext, const RuntimeMethod* method) 
{
	{
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)))((Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = ___0_observer;
		__this->___observer = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___observer), (void*)L_0);
		Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* L_1 = ___1_source;
		__this->___source = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source), (void*)L_1);
		SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0* L_2 = ___2_synchronizationContext;
		__this->___synchronizationContext = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___synchronizationContext), (void*)L_2);
		return;
	}
}
// Method Definition Index: 108495
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* _SubscribeOn_Run_mB80BFA99911C9D59997276EFCC66A8802EC6C734_gshared (_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* __this, const RuntimeMethod* method) 
{
	{
		SynchronizationContext_tCDB842BBE53B050802CBBB59C6E6DC45B5B06DC0* L_0 = __this->___synchronizationContext;
		il2cpp_codegen_runtime_class_init_inline(il2cpp_rgctx_data(method->klass->rgctx_data, 5));
		SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E* L_1 = ((_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->klass->rgctx_data, 5)))->___postCallback;
		NullCheck(L_0);
		VirtualActionInvoker2< SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E*, RuntimeObject* >::Invoke(5, L_0, L_1, (RuntimeObject*)__this);
		return (RuntimeObject*)__this;
	}
}
// Method Definition Index: 108496
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_Subscribe_m9ABF5D2EA6EF05E604120454351B58879F716D95_gshared (RuntimeObject* ___0_state, const RuntimeMethod* method) 
{
	_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* V_0 = NULL;
	{
		RuntimeObject* L_0 = ___0_state;
		V_0 = ((_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7*)CastclassSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 3)));
		_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* L_1 = V_0;
		NullCheck(L_1);
		SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* L_2 = (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8*)(&L_1->___disposable);
		_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* L_3 = V_0;
		NullCheck(L_3);
		Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* L_4 = L_3->___source;
		_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* L_5 = V_0;
		NullCheck(L_4);
		RuntimeObject* L_6;
		L_6 = ((  RuntimeObject* (*) (Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8*, Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(InitializedTypeInfo(method->klass)->rgctx_data, 6)))(L_4, (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)L_5, il2cpp_rgctx_method(InitializedTypeInfo(method->klass)->rgctx_data, 6));
		SingleAssignmentDisposableCore_set_Disposable_m512607DE32E18CFB7D692A8EE8C1BFE7DD2BB510(L_2, L_6, NULL);
		return;
	}
}
// Method Definition Index: 108497
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnNextCore_mFF3026A0ADBFD64E0EE953D91DB7EE91BB2D70B5_gshared (_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* __this, Il2CppFullySharedGenericAny ___0_value, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t25906970BCE8D4F5DA25ABDA05AB6A9A4BF60A2C = il2cpp_codegen_sizeof(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 7));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t25906970BCE8D4F5DA25ABDA05AB6A9A4BF60A2C);
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___observer;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 7)) ? ___0_value : &___0_value), SizeOf_T_t25906970BCE8D4F5DA25ABDA05AB6A9A4BF60A2C);
		NullCheck(L_0);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)), il2cpp_rgctx_method(method->klass->rgctx_data, 8), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 7)) ? L_1: *(void**)L_1));
		return;
	}
}
// Method Definition Index: 108498
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnErrorResumeCore_m3203DAC9D5799D2B9D8CE0908EE4B36B0B0D60A4_gshared (_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___observer;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Exception_t*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 9)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// Method Definition Index: 108499
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnCompletedCore_mA4F119526E14AEAE51E2E2A4210342C97AD75FF6_gshared (_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___observer;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 10)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		return;
	}
}
// Method Definition Index: 108500
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_DisposeCore_mB648830F9442E54D2E83FF19C5497509A9038D7E_gshared (_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7* __this, const RuntimeMethod* method) 
{
	{
		SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* L_0 = (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8*)(&__this->___disposable);
		SingleAssignmentDisposableCore_Dispose_m8FFF38AD98886810D7F28EF2C248F3DFF37513F0(L_0, NULL);
		return;
	}
}
// Method Definition Index: 108501
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn__cctor_mB6DB1BDD8832EBC8560E995D5593BF956127B9DC_gshared (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E* L_0 = (SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E*)il2cpp_codegen_object_new(SendOrPostCallback_t5C292A12062F24027A98492F52ECFE9802AA6F0E_il2cpp_TypeInfo_var);
		SendOrPostCallback__ctor_mE6F9D9606A00C3C18AEA057422ECF4106C80DA37(L_0, NULL, (intptr_t)((void*)il2cpp_rgctx_method(InitializedTypeInfo(method->klass)->rgctx_data, 11)), NULL);
		((_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 5)))->___postCallback = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((_SubscribeOn_t7742865A700104E155FC307995089D39EE0472C7_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 5)))->___postCallback), (void*)L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108504
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn__ctor_mF456A9F8653C840F719D236C63DB99BF4F9D8845_gshared (_SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25* __this, Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___0_observer, Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* ___1_source, const RuntimeMethod* method) 
{
	{
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)))((Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = ___0_observer;
		__this->___observer = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___observer), (void*)L_0);
		Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* L_1 = ___1_source;
		__this->___source = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source), (void*)L_1);
		return;
	}
}
// Method Definition Index: 108505
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* _SubscribeOn_Run_m18865E9F34D203704E10B7F32AB9A0A0E5F6250B_gshared (_SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ThreadPool_tCF41DF106471C552043F4C43B9881CA49A5D76CF_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(ThreadPool_tCF41DF106471C552043F4C43B9881CA49A5D76CF_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = ThreadPool_UnsafeQueueUserWorkItem_m084CD08E42EC545837C765F8FA2ACEBCD610DDD9((RuntimeObject*)__this, (bool)0, NULL);
		return (RuntimeObject*)__this;
	}
}
// Method Definition Index: 108506
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_Execute_m5B2A6AE12C9FA7F9821AA7FB9B0C98DBFC174C07_gshared (_SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25* __this, const RuntimeMethod* method) 
{
	Exception_t* V_0 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	try
	{
		SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* L_0 = (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8*)(&__this->___disposable);
		Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8* L_1 = __this->___source;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = ((  RuntimeObject* (*) (Observable_1_t83425A087FE3B63C66EE3AC9DCA253CD6378A7A8*, Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)))(L_1, (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		SingleAssignmentDisposableCore_set_Disposable_m512607DE32E18CFB7D692A8EE8C1BFE7DD2BB510(L_0, L_2, NULL);
		goto IL_002d;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0019;
		}
		throw e;
	}

CATCH_0019:
	{
		Exception_t* L_3 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));;
		V_0 = L_3;
		il2cpp_codegen_runtime_class_init_inline(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_il2cpp_TypeInfo_var)));
		Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04* L_4;
		L_4 = ObservableSystem_GetUnhandledExceptionHandler_mABD5FA2181B4365ED37D6813295F874BFEC6A77F_inline(NULL);
		Exception_t* L_5 = V_0;
		NullCheck(L_4);
		Action_1_Invoke_m43B5C4C0F292CE3E07CB03B46D8F960ACF7D6A58_inline(L_4, L_5, NULL);
		NullCheck((Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 6)))((Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002d;
	}

IL_002d:
	{
		return;
	}
}
// Method Definition Index: 108507
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnNextCore_m5E55331E734CD720C1A1524005093DDE2FC55F5F_gshared (_SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25* __this, Il2CppFullySharedGenericAny ___0_value, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t565A654595B6309CF454E2AFE5C5D61E78B09F86 = il2cpp_codegen_sizeof(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 7));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t565A654595B6309CF454E2AFE5C5D61E78B09F86);
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___observer;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 7)) ? ___0_value : &___0_value), SizeOf_T_t565A654595B6309CF454E2AFE5C5D61E78B09F86);
		NullCheck(L_0);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)), il2cpp_rgctx_method(method->klass->rgctx_data, 8), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 7)) ? L_1: *(void**)L_1));
		return;
	}
}
// Method Definition Index: 108508
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnErrorResumeCore_m6E4F2AA9DA6E896BFCFE86DE35C5D88890D523B9_gshared (_SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___observer;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Exception_t*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 9)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// Method Definition Index: 108509
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_OnCompletedCore_m78CD302CF471BE09249631449DE76A746A840F20_gshared (_SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___observer;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 10)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		return;
	}
}
// Method Definition Index: 108510
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _SubscribeOn_DisposeCore_m21D181F92326F9C2B01DFAFABC2F2DA0334AA981_gshared (_SubscribeOn_t220194769F4CB6C7150C870666371A73B5641C25* __this, const RuntimeMethod* method) 
{
	{
		SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8* L_0 = (SingleAssignmentDisposableCore_t3DB0991EE820A1AB2CC6163B3384E0A5F076B1D8*)(&__this->___disposable);
		SingleAssignmentDisposableCore_Dispose_m8FFF38AD98886810D7F28EF2C248F3DFF37513F0(L_0, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108513
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take__ctor_mF32FE01D811FA1EF758057F6BC17F811E0B670A8_gshared (_Take_tAC9861ABFEB04204FDE9EBE25D87AFDB0BFAB794* __this, Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___0_observer, int32_t ___1_count, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		int32_t L_1 = ___1_count;
		__this->___remaining = L_1;
		Observer_1__ctor_m35F6F15C7CE59E0B904DED1A3ED7EC9B917E4046((Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 2));
		return;
	}
}
// Method Definition Index: 108514
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take_OnNextCore_m9ADFE8D606A1E91A9B958C505A67FDE532A9FCBB_gshared (_Take_tAC9861ABFEB04204FDE9EBE25D87AFDB0BFAB794* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___remaining;
		if ((((int32_t)L_0) <= ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_1 = __this->___remaining;
		__this->___remaining = ((int32_t)il2cpp_codegen_subtract(L_1, 1));
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_2 = __this->___U3CobserverU3EP;
		RuntimeObject* L_3 = ___0_value;
		NullCheck(L_2);
		Observer_1_OnNext_mD7BB2434F471B0F546CEB286FBEF1D4D77FB0160(L_2, L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		int32_t L_4 = __this->___remaining;
		if (L_4)
		{
			goto IL_0036;
		}
	}
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_5 = __this->___U3CobserverU3EP;
		ObserverExtensions_OnCompleted_TisRuntimeObject_mB6C7AA41E77FB2A0CC3902D7CC5BF427214588AD(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
	}

IL_0036:
	{
		return;
	}
}
// Method Definition Index: 108515
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take_OnErrorResumeCore_mD6CD4294DF1F3ED1A0131CDE2EA709B7206DFF3E_gshared (_Take_tAC9861ABFEB04204FDE9EBE25D87AFDB0BFAB794* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		Observer_1_OnErrorResume_mD83E7C18B8617CE8185159FD248A8B43F441F18C(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
		return;
	}
}
// Method Definition Index: 108516
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take_OnCompletedCore_m19F1635C301004A78D12C3FC0C4E5DF064C4F0B8_gshared (_Take_tAC9861ABFEB04204FDE9EBE25D87AFDB0BFAB794* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		Observer_1_OnCompleted_mFC826873BB8CBFFF95D05B873EFEFC0CBC2EDAC0(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108513
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take__ctor_m133F5DD79A7ECF58A4532BDD4E50DEAE6AB84B98_gshared (_Take_t08AD65AC2F84512F4F4EC1985D42D832B58FDCD8* __this, Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___0_observer, int32_t ___1_count, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		int32_t L_1 = ___1_count;
		__this->___remaining = L_1;
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 2)))((Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 2));
		return;
	}
}
// Method Definition Index: 108514
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take_OnNextCore_m6C97EA772E00FB83DB8373040A9A8EEA216A4773_gshared (_Take_t08AD65AC2F84512F4F4EC1985D42D832B58FDCD8* __this, Il2CppFullySharedGenericAny ___0_value, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_tC7B926E1480874C818209D2BCA5F01F425929BAA = il2cpp_codegen_sizeof(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 4));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_tC7B926E1480874C818209D2BCA5F01F425929BAA);
	{
		int32_t L_0 = __this->___remaining;
		if ((((int32_t)L_0) <= ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_1 = __this->___remaining;
		__this->___remaining = ((int32_t)il2cpp_codegen_subtract(L_1, 1));
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_2 = __this->___U3CobserverU3EP;
		il2cpp_codegen_memcpy(L_3, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 4)) ? ___0_value : &___0_value), SizeOf_T_tC7B926E1480874C818209D2BCA5F01F425929BAA);
		NullCheck(L_2);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)), il2cpp_rgctx_method(method->klass->rgctx_data, 5), L_2, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 4)) ? L_3: *(void**)L_3));
		int32_t L_4 = __this->___remaining;
		if (L_4)
		{
			goto IL_0036;
		}
	}
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_5 = __this->___U3CobserverU3EP;
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 6)))(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
	}

IL_0036:
	{
		return;
	}
}
// Method Definition Index: 108515
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take_OnErrorResumeCore_m0C1B3C4CE0FCE9460240192321C4486275C28FBE_gshared (_Take_t08AD65AC2F84512F4F4EC1985D42D832B58FDCD8* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Exception_t*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 7)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
		return;
	}
}
// Method Definition Index: 108516
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Take_OnCompletedCore_m9F857FFA3B26B08CB5950D8057210647E415BD33_gshared (_Take_t08AD65AC2F84512F4F4EC1985D42D832B58FDCD8* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108519
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where__ctor_m93EE2100E46D14B55C41077AFF3B61C41005AF0A_gshared (_Where_tD6DDDF0FBCC547F4D1026D133796D380D1E4F5F9* __this, Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* ___0_observer, Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* ___1_predicate, const RuntimeMethod* method) 
{
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* L_1 = ___1_predicate;
		__this->___U3CpredicateU3EP = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CpredicateU3EP), (void*)L_1);
		Observer_1__ctor_mA04A589605CB06CEA6F1D6DC47272145ADEC30F6((Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 3));
		return;
	}
}
// Method Definition Index: 108520
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnNextCore_m204AE2936CDC5A5B686B4449CAA41D5479E6DCEA_gshared (_Where_tD6DDDF0FBCC547F4D1026D133796D380D1E4F5F9* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* L_0 = __this->___U3CpredicateU3EP;
		int32_t L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = Func_2_Invoke_m095D2006A2DDB336987862DC15A7EFAED53E08EC_inline(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		if (!L_2)
		{
			goto IL_001a;
		}
	}
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_3 = __this->___U3CobserverU3EP;
		int32_t L_4 = ___0_value;
		NullCheck(L_3);
		Observer_1_OnNext_m6C50488E4BC6D037E00920D35ABA85213E74B790(L_3, L_4, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
	}

IL_001a:
	{
		return;
	}
}
// Method Definition Index: 108521
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnErrorResumeCore_mE9FCDF55A94520C4DC9A6A21000AB1ABDCB20F3F_gshared (_Where_tD6DDDF0FBCC547F4D1026D133796D380D1E4F5F9* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		Observer_1_OnErrorResume_mDF6B7FABE356230FF4EC32E78828650B4FD45CA2(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
// Method Definition Index: 108522
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnCompletedCore_m0C623013C88706B9D25C385D401304EEC6F45B47_gshared (_Where_tD6DDDF0FBCC547F4D1026D133796D380D1E4F5F9* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		Observer_1_OnCompleted_m26C8A1108383678F346278A974F9A7DAC3CC2A0A(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108519
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where__ctor_mCAE31D3A847A51A40165759316B205941FF49C6C_gshared (_Where_t07B688566F1CE2EC195CF2412082E2BF789D59B5* __this, Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* ___0_observer, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___1_predicate, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___1_predicate;
		__this->___U3CpredicateU3EP = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CpredicateU3EP), (void*)L_1);
		Observer_1__ctor_m35F6F15C7CE59E0B904DED1A3ED7EC9B917E4046((Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 3));
		return;
	}
}
// Method Definition Index: 108520
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnNextCore_m5FF98BDDF52D0A93868475BA991EB0FCE2E453E5_gshared (_Where_t07B688566F1CE2EC195CF2412082E2BF789D59B5* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_0 = __this->___U3CpredicateU3EP;
		RuntimeObject* L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		if (!L_2)
		{
			goto IL_001a;
		}
	}
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_3 = __this->___U3CobserverU3EP;
		RuntimeObject* L_4 = ___0_value;
		NullCheck(L_3);
		Observer_1_OnNext_mD7BB2434F471B0F546CEB286FBEF1D4D77FB0160(L_3, L_4, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
	}

IL_001a:
	{
		return;
	}
}
// Method Definition Index: 108521
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnErrorResumeCore_m23C0F260F360DB352289D039175B8DF8C3246882_gshared (_Where_t07B688566F1CE2EC195CF2412082E2BF789D59B5* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		Observer_1_OnErrorResume_mD83E7C18B8617CE8185159FD248A8B43F441F18C(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
// Method Definition Index: 108522
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnCompletedCore_m2A3ACD8509B400DA3EE8888F2B64CFD18098C8F9_gshared (_Where_t07B688566F1CE2EC195CF2412082E2BF789D59B5* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		Observer_1_OnCompleted_mFC826873BB8CBFFF95D05B873EFEFC0CBC2EDAC0(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108519
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where__ctor_m2D2188E2D450556141914C402BC465C11B837D82_gshared (_Where_t9D34EE9F2F3CBD5E31620008200996D18315AD88* __this, Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___0_observer, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___1_predicate, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = ___1_predicate;
		__this->___U3CpredicateU3EP = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CpredicateU3EP), (void*)L_1);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 3)))((Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 3));
		return;
	}
}
// Method Definition Index: 108520
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnNextCore_m2EC23148674782F5B0BF118F4DC85193CDD344C1_gshared (_Where_t9D34EE9F2F3CBD5E31620008200996D18315AD88* __this, Il2CppFullySharedGenericAny ___0_value, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t00E952B4F6CA8D2E1FA07884E364E95B9627D4B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 5));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t00E952B4F6CA8D2E1FA07884E364E95B9627D4B3);
	const Il2CppFullySharedGenericAny L_4 = L_1;
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_0 = __this->___U3CpredicateU3EP;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 5)) ? ___0_value : &___0_value), SizeOf_T_t00E952B4F6CA8D2E1FA07884E364E95B9627D4B3);
		NullCheck(L_0);
		bool L_2;
		L_2 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 6)), il2cpp_rgctx_method(method->klass->rgctx_data, 6), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 5)) ? L_1: *(void**)L_1));
		if (!L_2)
		{
			goto IL_001a;
		}
	}
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_3 = __this->___U3CobserverU3EP;
		il2cpp_codegen_memcpy(L_4, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 5)) ? ___0_value : &___0_value), SizeOf_T_t00E952B4F6CA8D2E1FA07884E364E95B9627D4B3);
		NullCheck(L_3);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 7)), il2cpp_rgctx_method(method->klass->rgctx_data, 7), L_3, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 5)) ? L_4: *(void**)L_4));
	}

IL_001a:
	{
		return;
	}
}
// Method Definition Index: 108521
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnErrorResumeCore_m7D2A197DF4F2DECBE6A2B29D5224FF1E07C6CF17_gshared (_Where_t9D34EE9F2F3CBD5E31620008200996D18315AD88* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Exception_t*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
// Method Definition Index: 108522
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _Where_OnCompletedCore_mDF05A10C82AA51251D5CCCF354A18C7A999E6116_gshared (_Where_t9D34EE9F2F3CBD5E31620008200996D18315AD88* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 9)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108488
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect__ctor_mA968D0815BE509C9780977F687AB579D5BE4BC29_gshared (_WhereSelect_tCDFD2355712F51F0A6954F27E79B53E608FAC324* __this, Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* ___0_observer, Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* ___1_selector, Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* ___2_predicate, const RuntimeMethod* method) 
{
	{
		Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* L_1 = ___1_selector;
		__this->___U3CselectorU3EP = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CselectorU3EP), (void*)L_1);
		Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* L_2 = ___2_predicate;
		__this->___U3CpredicateU3EP = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CpredicateU3EP), (void*)L_2);
		Observer_1__ctor_mA04A589605CB06CEA6F1D6DC47272145ADEC30F6((Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 4));
		return;
	}
}
// Method Definition Index: 108489
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnNextCore_mE5595E4B0B44A67AD666886576094D5FB367A00A_gshared (_WhereSelect_tCDFD2355712F51F0A6954F27E79B53E608FAC324* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* L_0 = __this->___U3CpredicateU3EP;
		int32_t L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = Func_2_Invoke_m095D2006A2DDB336987862DC15A7EFAED53E08EC_inline(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
		if (!L_2)
		{
			goto IL_0025;
		}
	}
	{
		Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* L_3 = __this->___U3CobserverU3EP;
		Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* L_4 = __this->___U3CselectorU3EP;
		int32_t L_5 = ___0_value;
		NullCheck(L_4);
		bool L_6;
		L_6 = Func_2_Invoke_m095D2006A2DDB336987862DC15A7EFAED53E08EC_inline(L_4, L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		NullCheck(L_3);
		Observer_1_OnNext_m95727C008980996D1FA13041D3A987000792B120(L_3, L_6, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
	}

IL_0025:
	{
		return;
	}
}
// Method Definition Index: 108490
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnErrorResumeCore_m7D43A0AB4EF6DF7BF2E9008F35C37A7D2F612CC1_gshared (_WhereSelect_tCDFD2355712F51F0A6954F27E79B53E608FAC324* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		Observer_1_OnErrorResume_m092497C454071B749B9E18F43D74BAB28A146FA2(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
// Method Definition Index: 108491
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnCompletedCore_m038E3429EEBA232EFF6E643624A5BEB0489FC388_gshared (_WhereSelect_tCDFD2355712F51F0A6954F27E79B53E608FAC324* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_t58091ECFAACAC1EFAA343451FB61FDE35BAFC7FF* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		Observer_1_OnCompleted_m834BA0F85254B3C28E6A18FE22CA926AF3B268DB(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108488
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect__ctor_mCA9055638F0878077F30B5891686BE20A34FF73B_gshared (_WhereSelect_tD4006D864F2881A24658C4FB88E5D6D0D1C2DF1D* __this, Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* ___0_observer, Func_2_t213311159653563BDCC21CC060B449705C96791F* ___1_selector, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___2_predicate, const RuntimeMethod* method) 
{
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		Func_2_t213311159653563BDCC21CC060B449705C96791F* L_1 = ___1_selector;
		__this->___U3CselectorU3EP = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CselectorU3EP), (void*)L_1);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_2 = ___2_predicate;
		__this->___U3CpredicateU3EP = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CpredicateU3EP), (void*)L_2);
		Observer_1__ctor_m35F6F15C7CE59E0B904DED1A3ED7EC9B917E4046((Observer_1_tA5E8A69632AA04070F8D9F10C9AA07A88E4B4218*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 4));
		return;
	}
}
// Method Definition Index: 108489
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnNextCore_m95466D71B94293A71B4975E35D4E2C90F58362F9_gshared (_WhereSelect_tD4006D864F2881A24658C4FB88E5D6D0D1C2DF1D* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_0 = __this->___U3CpredicateU3EP;
		RuntimeObject* L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
		if (!L_2)
		{
			goto IL_0025;
		}
	}
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_3 = __this->___U3CobserverU3EP;
		Func_2_t213311159653563BDCC21CC060B449705C96791F* L_4 = __this->___U3CselectorU3EP;
		RuntimeObject* L_5 = ___0_value;
		NullCheck(L_4);
		int32_t L_6;
		L_6 = Func_2_Invoke_m1FDB82A936AD6A68F455DE792FD9454CE1A4FC9F_inline(L_4, L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		NullCheck(L_3);
		Observer_1_OnNext_m6C50488E4BC6D037E00920D35ABA85213E74B790(L_3, L_6, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
	}

IL_0025:
	{
		return;
	}
}
// Method Definition Index: 108490
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnErrorResumeCore_mC8222DCD1B1B7281B190421D506335F77D98F028_gshared (_WhereSelect_tD4006D864F2881A24658C4FB88E5D6D0D1C2DF1D* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		Observer_1_OnErrorResume_mDF6B7FABE356230FF4EC32E78828650B4FD45CA2(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
// Method Definition Index: 108491
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnCompletedCore_m5D3E913FEB2294C8D85C7289C8FFDCF1DDEFFB44_gshared (_WhereSelect_tD4006D864F2881A24658C4FB88E5D6D0D1C2DF1D* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_t29AC4E260059B626975FC65A65D89B02E2BCA041* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		Observer_1_OnCompleted_m26C8A1108383678F346278A974F9A7DAC3CC2A0A(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 108488
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect__ctor_m5892C08621A2DE0D91DFE6AAC8E60594AF611DF3_gshared (_WhereSelect_tA9DEB5E540178B4BE04D7FCBEB5DFC2AE5F1A98E* __this, Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* ___0_observer, Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* ___1_selector, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___2_predicate, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = ___0_observer;
		__this->___U3CobserverU3EP = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CobserverU3EP), (void*)L_0);
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_1 = ___1_selector;
		__this->___U3CselectorU3EP = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CselectorU3EP), (void*)L_1);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_2 = ___2_predicate;
		__this->___U3CpredicateU3EP = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CpredicateU3EP), (void*)L_2);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 4)))((Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 4));
		return;
	}
}
// Method Definition Index: 108489
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnNextCore_m21E0434DCF28B3EC09DAFA0094727A8199062347_gshared (_WhereSelect_tA9DEB5E540178B4BE04D7FCBEB5DFC2AE5F1A98E* __this, Il2CppFullySharedGenericAny ___0_value, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_tA42DE64B82DA9E61799E3941A8A5D8BA395A8F3E = il2cpp_codegen_sizeof(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 6));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_tA42DE64B82DA9E61799E3941A8A5D8BA395A8F3E);
	const Il2CppFullySharedGenericAny L_5 = L_1;
	const uint32_t SizeOf_TResult_tE62F8BDC705673C4522B8ACC47E6C2C97842A337 = il2cpp_codegen_sizeof(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 9));
	const Il2CppFullySharedGenericAny L_6 = alloca(SizeOf_TResult_tE62F8BDC705673C4522B8ACC47E6C2C97842A337);
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_0 = __this->___U3CpredicateU3EP;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 6)) ? ___0_value : &___0_value), SizeOf_T_tA42DE64B82DA9E61799E3941A8A5D8BA395A8F3E);
		NullCheck(L_0);
		bool L_2;
		L_2 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 7)), il2cpp_rgctx_method(method->klass->rgctx_data, 7), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 6)) ? L_1: *(void**)L_1));
		if (!L_2)
		{
			goto IL_0025;
		}
	}
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_3 = __this->___U3CobserverU3EP;
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_4 = __this->___U3CselectorU3EP;
		il2cpp_codegen_memcpy(L_5, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 6)) ? ___0_value : &___0_value), SizeOf_T_tA42DE64B82DA9E61799E3941A8A5D8BA395A8F3E);
		NullCheck(L_4);
		InvokerActionInvoker2< Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)), il2cpp_rgctx_method(method->klass->rgctx_data, 8), L_4, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 6)) ? L_5: *(void**)L_5), (Il2CppFullySharedGenericAny*)L_6);
		NullCheck(L_3);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 10)), il2cpp_rgctx_method(method->klass->rgctx_data, 10), L_3, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data_no_init(method->klass->rgctx_data, 9)) ? L_6: *(void**)L_6));
	}

IL_0025:
	{
		return;
	}
}
// Method Definition Index: 108490
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnErrorResumeCore_mE94736921AB61739DC904197917DFEE93C8B99E3_gshared (_WhereSelect_tA9DEB5E540178B4BE04D7FCBEB5DFC2AE5F1A98E* __this, Exception_t* ___0_error, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___U3CobserverU3EP;
		Exception_t* L_1 = ___0_error;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Exception_t*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 11)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
// Method Definition Index: 108491
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void _WhereSelect_OnCompletedCore_mF8FE9C6C5EDA157854691C4FA3C956D5B92CB28D_gshared (_WhereSelect_tA9DEB5E540178B4BE04D7FCBEB5DFC2AE5F1A98E* __this, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 ___0_result, const RuntimeMethod* method) 
{
	{
		Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E* L_0 = __this->___U3CobserverU3EP;
		Result_t1EEFFD204479E80A86CE4735CA81396E04B55412 L_1 = ___0_result;
		NullCheck(L_0);
		((  void (*) (Observer_1_tA6259B3DBAED4ED5CF966A5BFF18C22298F6881E*, Result_t1EEFFD204479E80A86CE4735CA81396E04B55412, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 12)))(L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 108434
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04* ObservableSystem_GetUnhandledExceptionHandler_mABD5FA2181B4365ED37D6813295F874BFEC6A77F_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_il2cpp_TypeInfo_var);
		Action_1_tAFBD759E01ADE1CCF9C2015D5EFB3E69A9F26F04* L_0 = ((ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_StaticFields*)il2cpp_codegen_static_fields_for(ObservableSystem_tD83E987AEF1790CA5881E85EDD71BE637792001E_il2cpp_TypeInfo_var))->___unhandledException;
		return L_0;
	}
}
// Method Definition Index: 888
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 912
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m095D2006A2DDB336987862DC15A7EFAED53E08EC_gshared_inline (Func_2_t1C8F983F9A1AA802D45E89037E2AA7ACD1094821* __this, int32_t ___0_arg, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_arg, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 912
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_gshared_inline (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* __this, RuntimeObject* ___0_arg, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_arg, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 912
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Func_2_Invoke_m1FDB82A936AD6A68F455DE792FD9454CE1A4FC9F_gshared_inline (Func_2_t213311159653563BDCC21CC060B449705C96791F* __this, RuntimeObject* ___0_arg, const RuntimeMethod* method) 
{
	typedef int32_t (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_arg, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
