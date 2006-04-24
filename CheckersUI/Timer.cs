using System;
using System.Runtime.InteropServices;


namespace Uberware.Gaming
{
  class Timer
  {
    
    #region Timer API
    
    [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
    [DllImport("kernel32")]
    static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);
    
    [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
    [DllImport("kernel32")]
    static extern bool QueryPerformanceCounter(ref long PerformanceCount);
    
    [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
    [DllImport("winmm.dll")]
    static extern int timeGetTime();
    
    #endregion
    
    private static bool isTimerInitialized = false;
    private static bool m_bUsingQPF = false;
    private static long m_llQPFTicksPerSec = 0;
    private static long m_llBaseTime = 0;
    private static double m_fBaseTime = 0;
    
    
    /// <summary>
    /// Returns the current pc time in seconds.
    /// Note that this value should be used relatively, not on its own since the source may vary.
    /// </summary>
    /// <returns></returns>
    public static float Query ()
    {
      if (!isTimerInitialized)
      {
        isTimerInitialized = true;
        
        // Use QueryPerformanceFrequency() to get frequency of timer.  If QPF is
        // not supported, we will timeGetTime() which returns milliseconds.
        long qwTicksPerSec = 0;
        m_bUsingQPF = QueryPerformanceFrequency(ref qwTicksPerSec);
        
        // Get base times
        if (m_bUsingQPF)
        {
          m_llQPFTicksPerSec = qwTicksPerSec;
          QueryPerformanceCounter(ref m_llBaseTime);
        }
        else
        {
          m_fBaseTime = (float)(timeGetTime() * 0.001);
        }
        
        // Return INIT
        return 0;
      }
      
      // !!!!! Check for overflow
      
      // Return elapsed time
      if (m_bUsingQPF)
      {
        long qwTime = 0;
        QueryPerformanceCounter(ref qwTime);
        return (float)( (double)(qwTime - m_llBaseTime) / (double)(m_llQPFTicksPerSec) );
      }
      else
      {
        double dTime = (timeGetTime() * 0.001);
        return (float)(dTime - m_fBaseTime);
      }
    }
  }
}
