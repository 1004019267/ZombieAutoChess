/// <summary>
/// 怪物的类型
/// </summary>
public enum eMonsterType
{
    Zombie_Throw=0,//僵尸投掷者
    Zombie_Bucket,//铁通僵尸
    Zombie_Master,//僵尸法师
    Zombie_Summoner,//僵尸召唤者
    Zombie_Strut,//撑杆僵尸
    Zombie_Doctor,//僵尸医生
    Zombie_Hammer,//巨锤僵尸
    Zombie_corrosion,//僵尸腐蚀者
    Zombie_Giant,//巨人僵尸
    Zombie_Snowman//雪人僵尸
}


public enum eSkillType
{
    Splash = 1,//溅射
    TauntAll, //群体嘲讽
    ZombieWave,//僵尸冲击波
    ReturnOfDead,//亡者归来
    JumpOverRail,//越过栏杆
    TreatmentAll,//群体治疗
    SquareRangeDamage,//范围群体打击
    DeadBoom,//死亡爆炸
    Ram,//猛撞
    SelfHealingAndDamageReduction//自愈减伤
}


/// <summary>
/// 游戏模式
/// </summary>
public enum eGame_mode
{
    None=0,
    Single,//单人模式
    Double//双人

}
/// <summary>
/// 阵营
/// </summary>
public enum eCamp
{
    Left=0,
    Right
}
/// <summary>
/// 游戏状态
/// </summary>
public enum eGame_State
{
    Matching=0,//匹配中
    Game_Start, //开始游戏
    Attack_ready, //准备战斗
    Attack_Begin,//战斗开始
    Attack_End,//战斗结束
    Game_Over,//游戏结束
    Pause,//暂停（仅限单人模式)
    Resum,//运行（仅限单人模式）  
}
/// <summary>
/// 地面btn
/// </summary>
public enum btn_ads
{
    plant_dimian_1,
    plant_dimian_2,
    plant_dimian_3,
    plant_dimian_4,
    plant_dimian_5,
    plant_dimian_6,
    plant_dimian_7,
    plant_dimian_8,
    plant_dimian_9,
    plant_dimian_10,
    plant_dimian_11,
    plant_dimian_12,
    plant_dimian_13,
    plant_dimian_14,
    plant_dimian_15,
    plant_dimian_16,
    plant_dimian_17,
    plant_dimian_18,
    plant_dimian_19,
    plant_dimian_20,
    plant_dimian_21,
    plant_dimian_22,
    plant_dimian_23,
    plant_dimian_24,
    plant_dimian_25,
}