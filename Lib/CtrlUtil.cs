using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Lib
{
    public class CtrlUtil
    {
        /// <summary>
        /// 鍵盤鍵入Enter將焦點移至另一控制項
        /// </summary>
        public static bool KeyEnterMoveFocus(KeyEventArgs e,
            FocusNavigationDirection focusDirection = FocusNavigationDirection.Next)
        {
            bool moveFocusSucc = false;
            if (e.Key != Key.Enter) return moveFocusSucc;
            TraversalRequest request = new TraversalRequest(focusDirection);
            UIElement focusElement = Keyboard.FocusedElement as UIElement;
            moveFocusSucc = focusElement?.MoveFocus(request) ?? false;
            return moveFocusSucc;
        }

        /// <summary>
        /// 即使 FrameworkElement IsEnabled=false，依然顯示 ToolTip
        /// </summary>
        public void SetToolTipShowOnDisabled()
        {
            ToolTipService.ShowOnDisabledProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(true));
        }

        /// <summary>
        /// 取得 Content Text
        /// </summary>
        /// <typeparam name="T">ContentPresenter 中尋找的類型</typeparam>
        /// <param name="content">Ex: ContentPresenter</param>
        /// <returns></returns>
        public string GetContentText<T>(FrameworkElement content)
            where T : FrameworkElement
        {
            string text = string.Empty;

            try
            {
                //if (cellContent.GetType() == typeof(ContentPresenter)) 
                if (content is ContentPresenter)
                    text = (FindContentFirstChild<T>(content) as dynamic)?.Text ?? string.Empty;
                else
                    text = (content as dynamic)?.Text ?? string.Empty;
            }
            catch (Exception) { }

            return text;
        }

        /// <summary>
        /// 尋找 Content 中第一個某類型的 FrameworkElement
        /// </summary>
        /// <typeparam name="T">尋找類型</typeparam>
        /// <param name="content">Ex: ContentPresenter</param>
        /// <returns></returns>
        public T FindContentFirstChild<T>(FrameworkElement content)
            where T : FrameworkElement
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(content);
            var children = new FrameworkElement[childrenCount];

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(content, i) as FrameworkElement;
                children[i] = child;
                if (child is T)
                    return (T)child;
            }

            for (int i = 0; i < childrenCount; i++)
                if (children[i] != null)
                {
                    var subChild = FindContentFirstChild<T>(children[i]);
                    if (subChild != null)
                        return subChild;
                }

            return null;
        }

        /// <summary>
        /// 定義 DataGrid columns
        /// </summary>
        /// <typeparam name="T">data source type</typeparam>
        /// <param name="dataGrid">DataGrid</param>
        /// <param name="cols">定義的欄位</param>
        public void DataGridDefineCols<T>(DataGrid dataGrid, HashSet<string> cols)
            where T : class
        {
            dataGrid.Columns.Clear();
            DataGridTextColumn txtCol;
            foreach (var col in cols)
            {
                txtCol = new DataGridTextColumn
                {
                    Header = ReflectionUtil.GetPropertyDisplayName<T>(col),
                    Binding = new Binding(col)
                };
                dataGrid.Columns.Add(txtCol);
            }
        }

        /// <summary>
        /// 取得 Window 實體
        /// </summary>
        /// <param name="typeName">
        /// EX:
        /// Controls.PdsRecdWindow、
        /// PdsWpfApp.LoginWindow
        /// </param>
        public Window GetWindow(string typeName, string titile = "")
        {
            Window win = null;
            Type targetType = null;
            string ns = string.Empty;

            try
            {
                if (typeName.Contains("."))
                    ns = typeName.Split('.')[0];

                // Dynamically loads an assembly.
                // 主程式不會編譯成 PdsWpfApp.dll，所以此段找不到
                targetType = Assembly.LoadFrom($"{ns}.dll").GetType(typeName);
            }
            catch (Exception) { }

            try
            {
                if (targetType == null)
                {
                    // Returns the assembly of the type by enumerating loaded assemblies in the app domain.
                    // 有可能尚未載入某些未使用的 Assembly，故此段用於尋找主程式 PdsWpfApp
                    Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                    targetType = loadedAssemblies.Select(assembly => assembly.GetType(typeName))
                        .Where(type => type != null)
                        .FirstOrDefault();
                }

                if (targetType != null)
                {
                    win = System.Activator.CreateInstance(targetType) as Window;
                    if ((!titile.IsNullOrWhiteSpace()) && win != null) win.Title = titile;
                }

            }
            catch (Exception) { }

            //return win?.ShowDialog() ?? false;
            return win;
        }

        /// <summary>
        /// 將文字內容加入至插入符號
        /// </summary>
        public void InsertTextAtCaret(TextBox textBox, string text)
        {
            text = text.NullableToStr();
            int insertStrLen = text.Length;
            int newCaretIndex = textBox.CaretIndex + insertStrLen;

            //  to insert text at the caret position
            textBox.Text = textBox.Text.Insert(textBox.CaretIndex, text);
            textBox.CaretIndex = newCaretIndex;

            // to scroll the textbox to the caret position
            int lineIndex = textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex);
            textBox.ScrollToLine(lineIndex);

            textBox.Focus();

            //// to replace the selected text with new text:
            //textBox.SelectedText = "<new text>";
        }

    }
}
