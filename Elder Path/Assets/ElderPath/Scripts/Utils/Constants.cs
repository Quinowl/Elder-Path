public static class Constants {

    // ===== MATERIAL PROPERTIES =====
    public const string MATERIAL_PROPERTY_MAIN_TEXTURE = "_MainTex";
    // ===== INPUTS =====
    public const string PLAYER_MOVE_ACTION = "Movement";
    public const string PLAYER_JUMP_ACTION = "Jump";
    public const string PLAYER_DASH_ACTION = "Dash";
    public const string PLAYER_ATTACK_ACTION = "Attack";
    public const string PLAYER_RESET_ACTION = "Reset";
    // ===== PLAYER ANIMATIONS =====
    public const string PLAYER_IDLE_ANIM = "player-idle";
    public const string PLAYER_RUN_ANIM = "player-run";
    public const string PLAYER_PUSH_ANIM = "player-push";
    public const string PLAYER_ATTACK_ANIM = "player-attack";
    public const string PLAYER_HIT_ANIM = "player-hit";
    public const string PLAYER_JUMP_ANIM = "player-jump-up";
    public const string PLAYER_FALLING_ANIM = "player-jump-down";
    public const string PLAYER_LAND_ANIM = "player-jump-grounded";
    // ===== PLAYER CONSTANTS FIELDS =====
    public const float PLAYER_GROUND_CHECK_DELAY_AFTER_JUMP = 0.25f;
    // ===== WALL ANIMATIONS =====
    public const string WALL_ANIMATION_CLOSED = "wall-closed";
    public const string WALL_ANIMATION_OPENING = "wall-opening";
    // ===== PRESSURE PLATE ANIMATIONS =====
    public const string PRESSURE_PLATE_IDLE = "pressure-plate-idle";
    public const string PRESSURE_PLATE_PRESSED = "pressure-plate-pressed";
    // ===== GENERAL ANIMATORS =====
    public const string COIN_ANIMATION_PICK_UP = "pickUp";
    // ===== TAGS =====
    public const string TAG_PLAYER = "Player";
    // ===== SCENES =====
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_GAME = "Game";
}