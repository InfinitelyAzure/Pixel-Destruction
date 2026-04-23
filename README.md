# Pixel Destruction
Bài test Unity Developer cho Wolffun Studio
Tổng quan kiến trúc project
Project được xây dựng xoay quanh 3 hệ thống chính: Damage System, Destruction System và Progression System.
Các hệ thống này tương tác với nhau thông qua Unity Events, giúp code dễ mở rộng và bảo trì.

## Destruction System
Đây là hệ thống cốt lõi của gameplay.
- Hệ thống sẽ đọc sprite theo từng pixel và chuyển đổi thành một lưới các chunk (ô vuông nhỏ).
- Mỗi chunk là một GameObject độc lập, có Rigidbody2D riêng
- Các chunk lân cận được liên kết với nhau bằng FixedJoint2D, tạo thành một cấu trúc thống nhất.
- Khi các liên kết bị phá vỡ, cấu trúc sẽ sụp đổ một cách tự nhiên theo vật lý.

## Damage System
Quản lý cách người chơi tương tác với môi trường.
- Bao gồm 2 cơ chế chính: tap/click và lưỡi cưa.
- Damage được áp dụng trực tiếp lên các chunk, làm suy yếu liên kết giữa chúng. Khi chịu đủ sát thương, các liên kết sẽ bị phá vỡ, khiến các phần của cấu trúc tách rời.
- Tap sử dụng cơ chế damage giảm dần theo khoảng cách, mạnh nhất ở tâm, yếu dần ra rìa, trong khi saw gây damage liên tục theo thời gian.

## Progression System
Kết nối gameplay với game flow tổng thể.
- Người chơi nhận EXP khi phá hủy các chunk.
- Khi lên level, người chơi sẽ chọn 1 trong 3 thẻ nâng cấp ngẫu nhiên.
- Các nâng cấp giúp tăng sức mạnh tạm thời như tăng damage, tăng bán kính, hoặc thêm lưỡi cưa mới
- Game hiện có 5 màn chơi với độ khó tăng dần.

## Hướng phát triển trong tương lai
Do giới hạn về thời gian, mình vẫn chưa hoàn thiện một số tính năng.
Nếu có thêm thời gian, mình sẽ ưu tiên:
- Tối ưu hiệu năng: Giảm thiễu các xử lý khi có nhiều chunk và physics cùng lúc. Tối ưu việc spawn và destroy object hợp lý hơn.
- Level Editor Tool
- Mở rộng hệ thống vũ khí: Thêm nhiều loại vũ khí và hướng nâng cấp.
- Cải thiện gameplay loop: Tăng chiều sâu cho progression. Cải thiện nhịp độ game và khả năng replay
  
---

# Download
[Android](https://github.com/InfinitelyAzure/Pixel-Destruction/releases/tag/AndroidDemo)

[PC](https://github.com/InfinitelyAzure/Pixel-Destruction/releases/tag/Demo)

---

# Pixel Destruction
Unity Developer Test for Wolffun Studio
Project Architecture Overview
The project is structured around three main systems: Damage System, Destruction System, and Progression System. These systems interact with each other through Unity Events, keeping the architecture modular and easy to extend.

## Destruction System
This is the core of the gameplay.
- The system analyzes the sprite pixel-by-pixel and converts it into a grid of small square chunk objects.
- Each chunk is an independent GameObject with its own physics body.
- Neighboring chunks are connected using FixedJoint2D, forming a cohesive structure.
- When connections break, the structure dynamically collapses in a physically believable way.

## Damage System
Handles how the player interacts with the environment.
- Includes two main mechanics: tap/click damage and saw damage.
- Damage is applied directly to chunks, weakening their connections.
- Once enough damage is dealt, joints break, causing parts of the structure to detach and fall.
- Tap damage uses a radial falloff (strongest at center, weaker at edges), while the saw applies continuous damage over time.

## Progression System
Bridges gameplay and overall game flow.
- Players gain EXP by destroying chunks.
- Upon leveling up, players can choose one of three random upgrade cards.
- Upgrades enhance abilities such as damage, radius, or adding new tools (e.g., additional saws).
- The game currently includes 5 stages with increasing difficulty.

## Future Improvements
Due to time constraints, some features were not fully implemented.
If given more time, I would prioritize:
- Performance Optimization
- Reduce overhead from large numbers of chunk objects and physics interactions
- Improve spawning and destruction efficiency
- Level Editor Tool
- Enable faster creation and iteration of destructible environments
- Expanded Weapon System
- Introduce more weapon types and upgrade paths
- Improve synergy between different tools
- Gameplay Loop Enhancements
- Add more depth to progression and player decision-making
- Improve pacing and replayability
