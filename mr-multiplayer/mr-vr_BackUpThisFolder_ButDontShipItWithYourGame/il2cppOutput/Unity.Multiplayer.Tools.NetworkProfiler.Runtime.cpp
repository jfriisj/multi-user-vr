#include "pch-cpp.hpp"





template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct GenericInterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (const RuntimeMethod* method, RuntimeObject* obj)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};

struct Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11;
struct Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079;
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
struct IReadOnlyDictionary_2_t7E602F68B800E9FD97AE5462BAAEB2B2A0A6A6FD;
struct IReadOnlyDictionary_2_t1C9E4278DD0992BFB53341CF2B1D31E871D5CB59;
struct IReadOnlyDictionary_2_t727071262B8B87C9F1C794CCE7F03282F35F8F4C;
struct IReadOnlyDictionary_2_tC190C0DF3FC65E46AC2382576C1FF9A793C41951;
struct IReadOnlyList_1_tACBB2E1C257871194507D0C10E16FD03C486E45A;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
struct IMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258;
struct INetworkAdapter_t1B8B28814CB6D39490CB0434F38E297A75B46AF0;
struct MethodInfo_t;
struct MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA;
struct MetricFactory_tD1472F5A6AC3B2A052EBDE1E022152830B518E14;
struct NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E;
struct String_t;
struct UnsubscribeFromAllAdapters_t2C59AA45D1C66736C7F8B20FD0A85FF292D43CD3;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NetworkAdapters_t920951156C811E1CA3946E5BC7885C8D51C9D1E2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C const RuntimeMethod* INetworkAdapter_GetComponent_TisIMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_m7765E89563B9852071A89ABE8C094E3A4155D645_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ProfilerAdapterEventListener_OnAdapterAdded_mE8FFB57D503CDB4FEB9BB80A62C489436689CD71_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ProfilerAdapterEventListener_OnAdapterRemoved_m2D96FF92DE1111A3CAB6B07E1852C80C1B03A6BB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ProfilerAdapterEventListener_OnMetricsReceived_m23E7098C67648C4CC541351C6ADEA74C638C6D97_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_tC31A2036100965F301C42B388772D5A31720F65E 
{
};
struct MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA  : public RuntimeObject
{
	RuntimeObject* ___m_Counters;
	RuntimeObject* ___m_Gauges;
	RuntimeObject* ___m_Timers;
	RuntimeObject* ___m_PayloadEvents;
	RuntimeObject* ___U3CMetricsU3Ek__BackingField;
	uint64_t ___U3CConnectionIdU3Ek__BackingField;
};
struct NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E  : public RuntimeObject
{
	MetricFactory_tD1472F5A6AC3B2A052EBDE1E022152830B518E14* ___m_MetricFactory;
};
struct ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3  : public RuntimeObject
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
struct IntPtr_t 
{
	void* ___m_value;
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
struct Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11  : public MulticastDelegate_t
{
};
struct Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079  : public MulticastDelegate_t
{
};
struct UnsubscribeFromAllAdapters_t2C59AA45D1C66736C7F8B20FD0A85FF292D43CD3  : public MulticastDelegate_t
{
};
struct ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_StaticFields
{
	bool ___s_Initialized;
	NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E* ___s_NetStatSerializer;
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
#ifdef __clang__
#pragma clang diagnostic pop
#endif


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;

inline void Action_1__ctor_m3880508FAD3D34B163F6EBF8E1292EDC8BC3BA11 (Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared)(__this, ___0_object, ___1_method, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UnsubscribeFromAllAdapters_t2C59AA45D1C66736C7F8B20FD0A85FF292D43CD3* NetworkAdapters_SubscribeToAll_m723AC3FD21865EC093EA0E18E00007642790850A (Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11* ___0_subscribeToAdapter, Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11* ___1_unsubscribeFromAdapter, const RuntimeMethod* method) ;
inline void Action_1__ctor_m4F86B44384B4643D150CD9065F7B672056D27145 (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared)(__this, ___0_object, ___1_method, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NetStatSerializer__ctor_m9C4C58C9E498B39BD958D5596470165CC74318F2 (NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 116420
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ProfilerAdapterEventListener_SubscribeToAdapterAndMetricEvents_mD5C1B513417F2C0D86A7851DE04261960EB523F3 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NetworkAdapters_t920951156C811E1CA3946E5BC7885C8D51C9D1E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProfilerAdapterEventListener_OnAdapterAdded_mE8FFB57D503CDB4FEB9BB80A62C489436689CD71_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProfilerAdapterEventListener_OnAdapterRemoved_m2D96FF92DE1111A3CAB6B07E1852C80C1B03A6BB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:18>
		il2cpp_codegen_runtime_class_init_inline(ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var);
		bool L_0 = ((ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_StaticFields*)il2cpp_codegen_static_fields_for(ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var))->___s_Initialized;
		if (L_0)
		{
			goto IL_002b;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:20>
		il2cpp_codegen_runtime_class_init_inline(ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var);
		((ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_StaticFields*)il2cpp_codegen_static_fields_for(ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var))->___s_Initialized = (bool)1;
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:21>
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:22>
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:23>
		Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11* L_1 = (Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11*)il2cpp_codegen_object_new(Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11_il2cpp_TypeInfo_var);
		Action_1__ctor_m3880508FAD3D34B163F6EBF8E1292EDC8BC3BA11(L_1, NULL, (intptr_t)((void*)ProfilerAdapterEventListener_OnAdapterAdded_mE8FFB57D503CDB4FEB9BB80A62C489436689CD71_RuntimeMethod_var), NULL);
		Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11* L_2 = (Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11*)il2cpp_codegen_object_new(Action_1_t8DDBAA92DE7828AE17B1CC89A0C19513FF23DB11_il2cpp_TypeInfo_var);
		Action_1__ctor_m3880508FAD3D34B163F6EBF8E1292EDC8BC3BA11(L_2, NULL, (intptr_t)((void*)ProfilerAdapterEventListener_OnAdapterRemoved_m2D96FF92DE1111A3CAB6B07E1852C80C1B03A6BB_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(NetworkAdapters_t920951156C811E1CA3946E5BC7885C8D51C9D1E2_il2cpp_TypeInfo_var);
		UnsubscribeFromAllAdapters_t2C59AA45D1C66736C7F8B20FD0A85FF292D43CD3* L_3;
		L_3 = NetworkAdapters_SubscribeToAll_m723AC3FD21865EC093EA0E18E00007642790850A(L_1, L_2, NULL);
	}

IL_002b:
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:25>
		return;
	}
}
// Method Definition Index: 116421
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ProfilerAdapterEventListener_OnAdapterAdded_mE8FFB57D503CDB4FEB9BB80A62C489436689CD71 (RuntimeObject* ___0_adapter, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&INetworkAdapter_GetComponent_TisIMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_m7765E89563B9852071A89ABE8C094E3A4155D645_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProfilerAdapterEventListener_OnMetricsReceived_m23E7098C67648C4CC541351C6ADEA74C638C6D97_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:29>
		RuntimeObject* L_0 = ___0_adapter;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = GenericInterfaceFuncInvoker0< RuntimeObject* >::Invoke(INetworkAdapter_GetComponent_TisIMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_m7765E89563B9852071A89ABE8C094E3A4155D645_RuntimeMethod_var, L_0);
		V_0 = L_1;
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:30>
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_001c;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:32>
		RuntimeObject* L_3 = V_0;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_4 = (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)il2cpp_codegen_object_new(Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var);
		Action_1__ctor_m4F86B44384B4643D150CD9065F7B672056D27145(L_4, NULL, (intptr_t)((void*)ProfilerAdapterEventListener_OnMetricsReceived_m23E7098C67648C4CC541351C6ADEA74C638C6D97_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		InterfaceActionInvoker1< Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* >::Invoke(0, IMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_il2cpp_TypeInfo_var, L_3, L_4);
	}

IL_001c:
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:34>
		return;
	}
}
// Method Definition Index: 116422
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ProfilerAdapterEventListener_OnAdapterRemoved_m2D96FF92DE1111A3CAB6B07E1852C80C1B03A6BB (RuntimeObject* ___0_adapter, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&INetworkAdapter_GetComponent_TisIMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_m7765E89563B9852071A89ABE8C094E3A4155D645_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProfilerAdapterEventListener_OnMetricsReceived_m23E7098C67648C4CC541351C6ADEA74C638C6D97_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:38>
		RuntimeObject* L_0 = ___0_adapter;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = GenericInterfaceFuncInvoker0< RuntimeObject* >::Invoke(INetworkAdapter_GetComponent_TisIMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_m7765E89563B9852071A89ABE8C094E3A4155D645_RuntimeMethod_var, L_0);
		V_0 = L_1;
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:39>
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_001c;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:41>
		RuntimeObject* L_3 = V_0;
		Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* L_4 = (Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079*)il2cpp_codegen_object_new(Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079_il2cpp_TypeInfo_var);
		Action_1__ctor_m4F86B44384B4643D150CD9065F7B672056D27145(L_4, NULL, (intptr_t)((void*)ProfilerAdapterEventListener_OnMetricsReceived_m23E7098C67648C4CC541351C6ADEA74C638C6D97_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		InterfaceActionInvoker1< Action_1_t76E00A62308B4884D5FBE7973531637E07E7B079* >::Invoke(1, IMetricCollectionEvent_tA72E299C92E8C93B12BA2474EB44722BE3EFF258_il2cpp_TypeInfo_var, L_3, L_4);
	}

IL_001c:
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:43>
		return;
	}
}
// Method Definition Index: 116423
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ProfilerAdapterEventListener_OnMetricsReceived_m23E7098C67648C4CC541351C6ADEA74C638C6D97 (MetricCollection_tC854AC2B17132ADE592D32683C1EF00AB6B2B8AA* ___0_metricCollection, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:48>
		return;
	}
}
// Method Definition Index: 116424
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ProfilerAdapterEventListener__cctor_m95F9F00DD1794CD7AE0D1578EB785861B798A8C9 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.multiplayer.tools@8395f16a9a9a/NetworkProfiler/Runtime/ProfilerAdapterEventListener.cs:50>
		NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E* L_0 = (NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E*)il2cpp_codegen_object_new(NetStatSerializer_t9F27B48ACF02B224520014E32721BDFE2B25697E_il2cpp_TypeInfo_var);
		NetStatSerializer__ctor_m9C4C58C9E498B39BD958D5596470165CC74318F2(L_0, NULL);
		((ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_StaticFields*)il2cpp_codegen_static_fields_for(ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var))->___s_NetStatSerializer = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_StaticFields*)il2cpp_codegen_static_fields_for(ProfilerAdapterEventListener_t8B16080549E55A9638611877E08003299BA642C3_il2cpp_TypeInfo_var))->___s_NetStatSerializer), (void*)L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
