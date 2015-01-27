using System;
using System.Collections.Generic;
using LeagueSharp;
using LeagueSharp.Common;

namespace ZedIsBalanced
{
    class Zed
    {
        private Menu _menu;
        private Orbwalking.Orbwalker _orbwalker;
        private Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        private delegate void OnOrbwalker();

        private Dictionary<Orbwalking.OrbwalkingMode, OnOrbwalker> _orbwalkerCallbacks;
        private Dictionary<SpellSlot, Spell> _spells = new Dictionary<SpellSlot, Spell>()
        {
            {SpellSlot.Q, new Spell(SpellSlot.Q, 900)},
            {SpellSlot.W, new Spell(SpellSlot.W, 550)},
            {SpellSlot.E, new Spell(SpellSlot.E, 290)},
            {SpellSlot.R, new Spell(SpellSlot.R, 625)}
        };

        private Dictionary<ShadowType, ShadowState> _shadows = new Dictionary<ShadowType, ShadowState>()
        {
            {ShadowType.Normal, ShadowState.NotActive},
            {ShadowType.Ult, ShadowState.NotActive}
        };

        public Zed()
        {
            _orbwalkerCallbacks = new Dictionary<Orbwalking.OrbwalkingMode, OnOrbwalker>()
            {
                {Orbwalking.OrbwalkingMode.Combo, OnCombo},
                {Orbwalking.OrbwalkingMode.Mixed, OnMixed},
                {Orbwalking.OrbwalkingMode.LastHit, OnLastHit},
                {Orbwalking.OrbwalkingMode.LaneClear, OnLaneClear},
                {Orbwalking.OrbwalkingMode.None, () => {}},
            };

            _spells[SpellSlot.Q].SetSkillshot(0.25f, 50f, 1700f, false, SkillshotType.SkillshotLine);

            BuildMenu();
            Game.OnGameUpdate += e =>
            {
                _orbwalkerCallbacks[_orbwalker.ActiveMode]();
            };
        }

        private void BuildMenu()
        {
            _menu = new Menu("Zed Is Balanced", "zisb", true);
            _orbwalker = new Orbwalking.Orbwalker(_menu.AddSubMenu(new Menu("Orbwalker", "zisb.orbwalker")));
            TargetSelector.AddToMenu(new Menu("Target Selector", "zisb.targetselect"));

            _menu.AddToMainMenu();
        }

        private void OnCombo() { }
        private void OnMixed() { }
        private void OnLastHit() { }
        private void OnLaneClear() { }

    }
}
