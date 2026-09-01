using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using System;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    /// <summary>
    /// 实时调节类 Dialog 的 ApeForms 基类：在 <see cref="ApeFormsDialog{TResult}"/> 之上实现 <see cref="IAdjustDialog{T}"/> 契约。
    /// </summary>
    /// <typeparam name="TResult">调节值类型</typeparam>
    public abstract class ApeFormsAdjustDialogBase<TResult> : ApeFormsDialog<TResult>, IAdjustDialog<TResult>
    {
        protected ApeFormsAdjustDialogBase(DialogSettings<TResult> settings, Control context = null, Control contentView = null)
            : base(settings, context, contentView)
        {
        }

        /// <inheritdoc/>
        public event EventHandler<ValueChangedEventArgs<TResult>> ValueChanged;

        /// <inheritdoc/>
        public abstract TResult Value { get; }

        /// <summary>
        /// 触发值变化（同时调用设置中的 <see cref="DialogSettings{TResult}.OnValueChanged"/> 委托）。
        /// </summary>
        protected void RaiseValueChanged(TResult newValue)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs<TResult>(newValue));
            Settings.OnValueChanged?.Invoke(newValue);
        }
    }
}