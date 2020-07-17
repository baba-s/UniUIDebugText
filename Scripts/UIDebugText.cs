using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
#pragma warning disable 0414

namespace Kogane.Internal
{
	/// <summary>
	/// デバッグテキストの UI を管理するクラス
	/// </summary>
	[AddComponentMenu( "" )]
	[DisallowMultipleComponent]
	public sealed class UIDebugText : MonoBehaviour
	{
		//====================================================================================
		// 定数
		//====================================================================================
		private const string DISABLE_CONDITION_STRING = "Hq9kSarVV5BXq4jNPzxj6kQEL8xqvyDr";

		//====================================================================================
		// 変数(SerializeField)
		//====================================================================================
		[SerializeField] private GameObject    m_closeBaseUI   = default;
		[SerializeField] private GameObject    m_openBaseUI    = default;
		[SerializeField] private Button        m_closeButtonUI = default;
		[SerializeField] private Button        m_openButtonUI  = default;
		[SerializeField] private CanvasGroup   m_canvasGroup   = default;
		[SerializeField] private GameObject    m_root          = default;
		[SerializeField] private RectTransform m_textBaseUI    = default;
		[SerializeField] private Text          m_textUI        = default;
		[SerializeField] private RectTransform m_textRectUI    = default;
		[SerializeField] private Vector2       m_sizeOffset    = default;

		//====================================================================================
		// 変数
		//====================================================================================
		private bool         m_isOpen;
		private string       m_currentText;
		private Vector2      m_currentTextSize;
		private Func<string> m_getText;
		private float        m_interval;
		private float        m_timer;

		//====================================================================================
		// 関数
		//====================================================================================
		/// <summary>
		/// 初期化される時に呼び出されます
		/// </summary>
		private void Awake()
		{
#if DISABLE_UNI_UI_DEBUG_TEXT
			Destroy( gameObject );
#else
			m_closeButtonUI.onClick.AddListener( () => SetState( false ) );
			m_openButtonUI.onClick.AddListener( () => SetState( true ) );
#endif
		}
		
#if DISABLE_UNI_UI_DEBUG_TEXT
#else
		/// <summary>
		/// 開始する時に呼び出されます
		/// </summary>
		private void Start()
		{
			m_root.SetActive( true );
			m_openBaseUI.SetActive( false );
			m_closeBaseUI.SetActive( true );

			SetState( false );
		}

		/// <summary>
		/// 更新される時に呼び出されます
		/// </summary>
		private void Update()
		{
			if ( !m_isOpen ) return;
			if ( m_interval <= 0 ) return;

			m_timer += Time.unscaledDeltaTime;

			if ( m_timer < m_interval ) return;

			m_timer -= m_interval;

			UpdateText();
			UpdateSize();
		}

#endif

		/// <summary>
		/// ステートを設定します
		/// </summary>
#if DISABLE_UNI_UI_DEBUG_TEXT
		[Conditional( DISABLE_CONDITION_STRING )]
#endif
		private void SetState( bool isOpen )
		{
			m_isOpen = isOpen;

			m_openBaseUI.SetActive( isOpen );
			m_closeBaseUI.SetActive( !isOpen );

			if ( !isOpen ) return;

			UpdateText();
			UpdateSize();
		}

		/// <summary>
		/// 表示するかどうかを設定します
		/// </summary>
#if DISABLE_UNI_UI_DEBUG_TEXT
		[Conditional( DISABLE_CONDITION_STRING )]
#endif
		public void SetVisible( bool isVisible )
		{
			var alpha = isVisible ? 1 : 0;
			m_canvasGroup.alpha = alpha;
		}

		/// <summary>
		/// 表示を設定します
		/// </summary>
#if DISABLE_UNI_UI_DEBUG_TEXT
		[Conditional( DISABLE_CONDITION_STRING )]
#endif
		public void SetDisp( string text )
		{
			SetDisp( 0, () => text );
		}

		/// <summary>
		/// 表示を設定します
		/// </summary>
#if DISABLE_UNI_UI_DEBUG_TEXT
		[Conditional( DISABLE_CONDITION_STRING )]
#endif
		public void SetDisp( float interval, Func<string> getText )
		{
			m_interval = interval;
			m_getText  = getText;

			UpdateText();
			UpdateSize();
		}

		/// <summary>
		/// テキストを更新します
		/// </summary>
		private void UpdateText()
		{
			if ( !m_isOpen ) return;

			var text = m_getText();

			if ( m_textUI.text == text ) return;

			m_textUI.text = text;
		}

		/// <summary>
		/// 描画範囲を更新します
		/// </summary>
		private void UpdateSize()
		{
			StartCoroutine( DoUpdateSize() );
		}

		/// <summary>
		/// 描画範囲を更新します
		/// </summary>
		private IEnumerator DoUpdateSize()
		{
			yield return null;

			if ( !m_isOpen ) yield break;

			var textSize = m_textRectUI.sizeDelta;

			if ( textSize == m_currentTextSize ) yield break;

			var textBaseSize = textSize + m_sizeOffset;

			m_textBaseUI.sizeDelta = textBaseSize;
			m_currentTextSize      = textSize;
		}
	}
}