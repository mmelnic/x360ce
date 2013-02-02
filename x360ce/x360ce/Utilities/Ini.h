/*  x360ce - XBOX360 Controler Emulator
*  Copyright (C) 2002-2010 Racer_S
*  Copyright (C) 2010-2011 Robert Krawczyk
*
*  x360ce is free software: you can redistribute it and/or modify it under the terms
*  of the GNU Lesser General Public License as published by the Free Software Found-
*  ation, either version 3 of the License, or (at your option) any later version.
*
*  x360ce is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
*  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
*  PURPOSE.  See the GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License along with x360ce.
*  If not, see <http://www.gnu.org/licenses/>.
*/

#ifndef _INI_H_
#define _INI_H_

#include "globals.h"
#include <Shlwapi.h>
#include "Utilities\CriticalSection.h"
#include <string.h>

class Ini
{
public:

    Ini(const char* ininame)
    {
        Mutex();
        char strPath[MAX_PATH];
        char tmp[MAX_PATH];

        GetModuleFileNameA(NULL, strPath, MAX_PATH);
        PathRemoveFileSpecA(strPath);
        PathAddBackslashA(strPath);

        sprintf_s(tmp,MAX_PATH,"%s%s",strPath, ininame);
        ini_file = tmp;
    }
    virtual ~Ini(void) {}

    static CriticalSection& Mutex()
    {
        static CriticalSection mutex;
        return mutex;
    }

    DWORD GetString(const char* strFileSection, const char* strKey, char* strOutput, char* strDefault = 0)
    {
        if(ini_file.empty()) return 0;

        Mutex().Lock();

        DWORD ret;
        char* pStr;
        ret = GetPrivateProfileStringA(strFileSection, strKey, strDefault, strOutput, MAX_PATH, ini_file.c_str());

        pStr = strchr(strOutput, L'#');
        if (pStr) *pStr=L'\0';
        pStr = strchr(strOutput, L';');
        if (pStr) *pStr=L'\0';

        Mutex().Unlock();

        return ret;
    }

    long GetLong(const char* strFileSection, const char* strKey, INT iDefault = 0)
    {
        char tmp[MAX_PATH];
        char def[MAX_PATH];
        DWORD ret;

        sprintf_s(def,MAX_PATH,"%d",iDefault);
        ret = GetString(strFileSection,strKey,tmp,def);

        if(ret) return strtol(tmp,NULL,0);
        return 0;
    }

    DWORD GetDword(const char* strFileSection, const char* strKey, INT iDefault = 0)
    {
        char tmp[MAX_PATH];
        char def[MAX_PATH];
        DWORD ret;

        sprintf_s(def,MAX_PATH,"%d",iDefault);
        ret = GetString(strFileSection,strKey,tmp,def);

        if(ret) return strtoul(tmp,NULL,0);
        return 0;
    }

    bool GetBool(const char* strFileSection, const char* strKey, INT iDefault = 0)
    {
        return GetDword(strFileSection,strKey,iDefault) !=0;
    }

private:
    std::string ini_file;
};

#endif // _INI_H_
