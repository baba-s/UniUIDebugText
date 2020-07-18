# UniUIDebugText

デバッグ用のテキスト表示を簡単に実装できる UI

## 使用例

![2020-07-18_093523](https://user-images.githubusercontent.com/6134875/87840316-0aedbe00-c8da-11ea-8d43-2cef2a1dc1b5.png)

「UIDebugText」プレハブをシーンに配置して  

```cs
using Kogane;
using System.Text;
using UnityEngine;

public class Example : MonoBehaviour
{
    public UIDebugText m_debugTextUI;

    private int m_value;

    private void Start()
    {
        // 第 1 引数：描画を更新する間隔（秒）
        // 第 2 引数：描画するテキスト
        m_debugTextUI.SetDisp
        (
            interval: 1,
            getText: () =>
            {
                var sb = new StringBuilder();
                sb.AppendLine( $"Frame: {m_value}" );
                sb.AppendLine( $"Version: {Application.version}" );
                sb.AppendLine( $"Debug: {Debug.isDebugBuild}" );
                sb.Append( $"Unity Pro: {Application.HasProLicense()}" );
                return sb.ToString();
            }
        );
    }

    private void Update()
    {
        m_value++;
    }
}
```

上記のようなスクリプトを記述することで  

![18](https://user-images.githubusercontent.com/6134875/87840349-3c668980-c8da-11ea-8f6b-eb615a8b2bce.gif)

使用することができます  

![2020-07-18_093940](https://user-images.githubusercontent.com/6134875/87840412-88b1c980-c8da-11ea-91dd-21fd239dc315.png)

`DISABLE_UNI_UI_DEBUG_TEXT` シンボルを定義することで UniUIDebugText を無効化できます  
リリースビルドから UniUIDebugText を除外したい場合などに定義します  
