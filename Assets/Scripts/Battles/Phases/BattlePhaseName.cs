namespace RineaR.Shadow.Battles.Phases
{
    /// <summary>
    ///     試合のフェーズ。
    /// </summary>
    public enum BattlePhaseName
    {
        /// <summary>
        ///     まだ開始していない状態。
        /// </summary>
        NotStarted,

        /// <summary>
        ///     試合を始めるための準備をしているフェーズ。
        /// </summary>
        Ready,

        /// <summary>
        ///     プレイヤーが、そのターンの行動を選択しているフェーズ。
        /// </summary>
        Operate,

        /// <summary>
        ///     選択された行動が実行されているフェーズ。
        /// </summary>
        Act,

        /// <summary>
        ///     ゲームセット後のフェーズ。
        /// </summary>
        GameSet,
    }
}