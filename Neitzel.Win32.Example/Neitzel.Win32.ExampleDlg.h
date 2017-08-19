
// Neitzel.Win32.ExampleDlg.h : header file
//

#pragma once


// CNeitzelWin32ExampleDlg dialog
class CNeitzelWin32ExampleDlg : public CDialogEx
{
// Construction
public:
	CNeitzelWin32ExampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_NEITZELWIN32EXAMPLE_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButton();
	afx_msg void OnBnClickedOk();
};
