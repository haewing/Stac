using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSM
{
    public delegate void DelegateFunc();
    public class CState
    {
        public DelegateFunc m_OnEnterFunc = null;
        public DelegateFunc m_OnExitFunc = null;
        public virtual void Initialize(DelegateFunc func)
        {
            m_OnEnterFunc = new DelegateFunc(func);
        }

        public virtual void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public virtual void OnUpdate() { }
        public virtual void OnExit()
        {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }

    }


    public class CReadyState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() { }
    }

    public class CWaveState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() { }
    }
    public class CGameState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() { }
    }
    public class CResultState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate() { }
        public override void OnExit() { }
    }

    private CState m_curState = null;
    private CState m_newState = null;

    private CState m_kReady = new CReadyState();
    private CState m_kWave = new CWaveState();
    private CState m_kGame = new CGameState();
    private CState m_kResult = new CResultState();

    //public CState readyState { get {return m_kReady; } }
    //public CState waveState { get {return m_kWave; } }
    //public CState gameState { get {return m_kGame; } }
    //public CState resultState { get {return m_kResult; } }
    public void Initalize(DelegateFunc kReady, DelegateFunc kWave, DelegateFunc kGame, DelegateFunc kResult)
    {
        m_kReady.Initialize(kReady);
        m_kWave.Initialize(kWave);
        m_kGame.Initialize(kGame);
        m_kResult.Initialize(kResult);
    }

    //상태 변화 셋팅

    public void SetState(CState kState)
    {
        m_newState = kState;
    }

    public void OnUpdatae()
    {
        if (m_newState != null)
        {
            if (m_curState != null)
                m_curState.OnExit();

            m_curState = m_newState;
            m_newState = null;
            m_curState.OnEnter();
        }

        if (m_curState != null)
            m_curState.OnUpdate();
    }

    public void SetReadyState()
    {
        SetState(m_kReady);
    }
    public void SetWaveState()
    {
        SetState(m_kWave);
    }
    public void SetGameState()
    {
        SetState(m_kGame);
    }
    public void SetResultState()
    {
        SetState(m_kResult);
    }

    public bool IsCurState(CState kState)
    {
        if (m_curState == null)
            return false;

        return (m_curState == kState) ? true : false;
    }

    public CState GetCurState()
    {
        return m_curState;
    }
    public void SetNoneState()
    {
        m_newState = null;
        m_curState = null;
    }

    public bool IsResultState(CState kState)
    {
        return (m_curState == m_kResult) ? true : false;
    }
    public bool IsNoneState()
    {
        return (m_curState == null) ? true : false;
    }
    public bool IsGameState()
    {
        return (m_curState == m_kGame) ? true : false;
    }
}
