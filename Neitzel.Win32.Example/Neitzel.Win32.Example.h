
// Neitzel.Win32.Example.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CNeitzelWin32ExampleApp:
// See Neitzel.Win32.Example.cpp for the implementation of this class
//

class CNeitzelWin32ExampleApp : public CWinApp
{
public:
	CNeitzelWin32ExampleApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CNeitzelWin32ExampleApp theApp;