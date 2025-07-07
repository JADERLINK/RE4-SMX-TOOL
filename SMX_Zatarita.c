struct TagVec {
  float x, y, z;

  // If ps2/07
  float w;
};

struct FREE_ROTATE {
  TagVec Rot;
  uint8_t m_Flag;
};

struct FREE_SWING {
  float m_StartZ, m_RangeZ, m_SpeedZ;
  float m_Time;
  float m_StartX, m_RangeX, M_SpeedX;
  float m_StartY, m_RangeY, M_SpeedY;
  float m_InitAngX, m_InitAngY, m_InitAngZ;
};

struct FREE_PSWING {
  TagVec m_BasePos, m_Start, m_Range, m_Speed, m_Time;
};

struct GXColor {
  uint8_t r, g, b;
  uint8_t a; // (enum BLENDING_TYPES)
};


enum BLENDING_TYPES {
  NORMAL             = 0,
  ADD                = 1,
  ADD2               = 2,
  ADD3               = 3,
  NO_BLEND           = 4,
  LOCK_UNKNOWN_MODEL = 5
};

enum SMX_FLAGS {
  SHADOW              = 1,   // 0x01  0b00000001
  VERTEX_COLOR        = 2,   // 0x02  0b00000010
  CAST_ON             = 4,   // 0x04  0b00000100
  NO_ALPHA            = 8,   // 0x08  0b00001000
  IGNORE_POINT_LIGHTS = 16,  // 0x10  0b00010000
  IGNORE_FLASH_LIGHT  = 32   // 0x20  0b00100000
};

enum CULL_MODE {
  BACK    = 0,
  FRONT   = 1,
  NO_CULL = 2
};

// Used for OtType in cSmxWork
enum OT_TYPES {
  NORMAL      = 0,
  SORT        = 1,
  SMD_BEFORE  = 2,
  SMD_NORMAL  = 3,
  SMD_AFTER   = 4,
  EFFECT      = 5
};

enum MOVE_TYPE {
  TYPE_NORMAL     = 0,
  TYPE_ROTATE     = 1,  // struct FREE_ROTATE
  TYPE_SWING_ROT  = 2,  // struct FREE_SWING
  TYPE_SWING_POS  = 3,  // struct FREE_PSWING, unused
  TYPE_BIOCHAMBER = 4,  // unused
  TYPE_MIRROR     = 15, // unused
};


struct cSmxData {  // Header
  uint8_t Version; // 0x10
  uint8_t nData;   //Amount
  uint16_t pad_01;
  uint32_t pad_02[3];
};

struct cSmxWork {   // Entry
  uint8_t ModelNo;  // Index SMX_ID
  uint8_t Id;       // Type (enum MOVE_TYPE)
  uint8_t OtType;   // Render Heirarchy (enum OT_TYPES)
  uint8_t CullMode; // (enum CULL_MODE)
  uint32_t LitSelectMask;
  uint32_t Flag;    // (enum SMX_FLAGS)

  // Alpha in MaterialColor is BlendingTypes
  GXColor MaterialColor;
  
  // Union has the shared parameters that change based off Id(type)
  union {
    FREE_ROTATE rotate;            // MOVE_TYPE = 1
    TYPE_SWING_ROT swing_rotate;   // MOVE_TYPE = 2
    TYPE_SWING_POS swing_position; // MOVE_TYPE = 3
    unsigned char data[116];  // Size of the union
  }

  // Alpha in SpecularColor is BlendingTypes
  GXColor SpecularColor;

  float TexU;
  float TexV;
};

/*
I included some normal names in the comments for the Smx entries. As the code uses really janky names.
I think this should be everything that is in the extracted structures in the stabs list (ps2 debug iso)
By zatarita
/*