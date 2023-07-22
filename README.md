# Tank

## ToDo
### Performance

1. Tank 行进应该是动画效果而不是静态图片；(Player 修改完成， Enemy 还未修改)
2. 地图占位判断使用二维数组 bool 来判定，取代之前的一位无序数组查找；(Completed)
3. 

### Functionality

1. 开始界面是慢慢自下而上移动上来的；
2. 实现两个玩家的功能，第一个玩家使用WSAD ，第二个玩家使用上下左右；
3. 其他敌人类型添加；
4. 奖励机制；
5. 画布根据屏幕大小自适应缩放；

## Achieve

1. 实现开始选择界面和游戏界面；
2. 添加了基本音效；
3. Player 和 Enemy 坦克运动和攻击逻辑；
4. 加入了基本地图实体，包括 Wall, Barrier, Grass, River, Heart 等，并设置了碰撞属性；
5. 添加了玩家生命值和积分功能，并使用 UI 显示；
6. 玩家行进改为动画效果而非静态图片；

