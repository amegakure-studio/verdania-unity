<p align="center">
  <img alt="Verdania logo" width="500" src="https://github.com/amegakure-studio/verdania-cairo/assets/58611754/ece7df02-b1eb-4aed-a808-51f42c854c78">
</p>

<p align="center">
    <img alt="Amegakure logo" width="360" src="https://github.com/amegakure-studio/verdania-cairo/assets/58611754/8de7db30-91ef-493f-8ce0-8c34e66209bb">
</p>

<div align="center">
<a href=""><img src="https://img.shields.io/github/license/keep-starknet-strange/unruggable.meme.svg?style=for-the-badge" alt="Project license" height="30"></a>
<a href="https://twitter.com/0xVerdania"><img src="https://img.shields.io/twitter/follow/0xVerdania?style=for-the-badge&logo=twitter" alt="Follow 0xVerdania on Twitter" height="30"></a>
</div>

## 🌿 Verdania Unity Client 🌿 
## Useful Links

- [Trailer](https://www.youtube.com/)
- [Gameplay](https://www.youtube.com/)
- [Verdania on Taikai](https://taikai.network/starkware/hackathons/starknet-winter-hackathon)
- [Repository Verdania Cairo](https://github.com/amegakure-studio/verdania-cairo)

## Screenshots

![Screenshot from 2024-02-15 18-43-37](https://github.com/amegakure-studio/verdania-cairo/assets/58611754/91e63ca0-8658-41b7-9cf7-c6abf1421404)
![Screenshot from 2024-02-15 18-44-15](https://github.com/amegakure-studio/verdania-cairo/assets/58611754/ecedc7b2-1f08-47bf-bcd2-f29d914138b4)

## Current Development

The game features a frontend implemented in Unity, while the backend is built with Dojo on Starknet.

Currently, JPS is implemented and working, but due to the algorithm's poor performance, we decided to remove it. Therefore, the game's pathfinding is being handled by Unity. However, player positions are being persisted by Dojo.

## Used Libraries

The development of Verdania has benefited from several open-source libraries, including:

- [Dojo.Unity](https://github.com/dojoengine/dojo.unity): Used to facilitate the connection between Dojo and Unity.

## Mechanics
### Movement
Our game is fully on-chain. This means that we have the player's position registered on the farm. When the player wants to move to another tile, this action is also calculated on-chain.

To achieve this, we have implemented a pathfinding algorithm called JPS, which outperforms other algorithms such as Dijkstra and A*. This algorithm determines if there is a possible path for the player's movement.

### Interact
* Prepare the soil
* Sow/Plant
* Water
* Harvest

### Crop growth
Our crops include details on the time needed for them to be ready for harvest and their maximum survival time without irrigation. Additionally, each plant has a growth level ranging from 0 to 100, where 0 indicates it has just been planted and 100 means it's ready to be harvested.

The game's time is based on the blockchain's time, so the growth of the plants is determined by the time you spend caring for them.

Our 'updater' system handles efficiently updating active users' crops, taking into account the specific state of each crop. This system is triggered periodically via a cronjob.

### Marketplace
We utilize implementations of the `ERC20` and `ERC1155` standards from [Origami](https://github.com/dojoengine/origami), which we've adapted to suit our needs. Additionally, we've developed an `escrow contract` that serves as our marketplace.

This enables us to have an ERC20 token named `VRD`, which is the in-game currency used to purchase items. For the items themselves, we employ the ERC1155 standard.

Within the marketplace, users can list their items or purchase those already available for sale. Furthermore, thanks to this implementation, we can send both `VRD` and items to our friends.


## Assets

| Category | Asset | Link |
|----------|-------|------|
| SDK      | dojo.unity SDK | [GitHub Repository](https://github.com/dojoengine/dojo.unity) |
| BackEnd      | verdania Cairo | [GitHub Repository](https://github.com/amegakure-studio/verdania-cairo) |
| 3D models      | Characters | [VRoid Studio](https://vroid.com/en/studio) |
| 3D models      | Environment | [VRoid Studio]([https://vroid.com/en/studio](https://www.cgtrader.com/3d-models/exterior/cityscape/fantastic-village-pack)) |



## Supporters

Join us as Supporters and be an active part of Verdania's growth and continuous development. Your support is essential to take the game to new heights. Join [here](https://twitter.com/0xVerdania) and be part of this exciting adventure!

## Creators ✨
Thanks goes to these wonderful people
([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<table>
  <tbody>
    <tr>
    <td align="center" valign="top" width="14.28%"><a href="https://github.com/dubzn"><img src="https://avatars.githubusercontent.com/u/58611754?s=400&u=cdb4e29d9ac5bc41e7ee171375e8cd10fe8c3c24&v=4" width="100px;" alt="Dubzn"/><br /><sub><b>dubzn</b></sub></a><br />😸</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/dpinones"><img src="https://avatars.githubusercontent.com/u/30808181?v=4" width="100px;" alt="Damián Piñones"/><br /><sub><b>Damián Piñones</b></sub></a><br />🤠</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/cristianFleita"><img src="https://avatars.githubusercontent.com/u/87950451?v=4" width="100px;" alt="Cristian Fleita"/><br /><sub><b>Cristian Fleita</b></sub></a><br />💻</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/brendaamareco"><img src="https://avatars.githubusercontent.com/u/107716199?v=4" width="100px;" alt="Brenda Mareco"/><br /><sub><b>Brenda Mareco</b></sub></a><br />🎨</a></td>
    </tr>
</tbody>
</table>
