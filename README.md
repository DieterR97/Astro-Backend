<!-- HEADER SECTION -->
<h5 align="center" style="padding:0;margin:0;">Dieter | Erik | Ungerer | Mariné | Hannah</h5>
<h5 align="center" style="padding:0;margin:0;">221122 | 221147 | 221302 | 221326 | 21100366</h5>
<h6 align="center">Interactive Development 300</h6>
<p align="center">
    <img src="astro-backend/assets/Logo.jpg" alt="Logo" width="80" height="80">
  <h3 align="center">Astro</h3>
  <p align="center">
    Cross-Platform Desktop Development <br>
      <a href="https://github.com/DieterR97/Astro"><strong>Explore the docs »</strong></a>
   <br />
   <br />
    <a href="https://drive.google.com/file/d/1v8MVrl1g98yQJLvkuEm_BxsAA0NvnWFn/view?usp=sharing">View Demo</a>
    ·
    <a href="https://github.com/DieterR97/Astro/issues">Report Bug</a>
    ·
    <a href="https://github.com/DieterR97/Astro/issues">Request Feature</a>
</p>
<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Project Description](#project-description)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [How to install](#how-to-install)
* [Features and Functionality](#features-and-functionality)
* [Concept Process](#concept-process)
   * [Ideation](#ideation)
   * [Wireframes](#wireframes)
   * [User-flow](#user-flow)
* [Development Process](#development-process)
   * [Implementation Process](#implementation-process)
        * [Highlights](#highlights)
        * [Challenges](#challenges)
   * [Future Implementation](#peer-reviews)
* [Final Outcome](#final-outcome)
    * [Mockups](#mockups)
    * [Video Demonstration](#video-demonstration)
* [Conclusion](#conclusion)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)

<!--PROJECT DESCRIPTION-->
## About the Project
<!-- header image of project -->
![Mockup1](astro-backend/assets/mockup_one.jpg)

### Project Description

Develop a cross-platform desktop application simulating an e-banking system. This system will allow account holders to manage their finances, including purchasing specific currencies, making transactions with other account holders, and withdrawing funds.

### Built With

* <a href="https://www.electronjs.org/"> ![Static Badge](https://img.shields.io/badge/electron-url?style=for-the-badge&logo=electron&logoColor=lightblue&color=grey) </a>
* <a href="https://dotnet.microsoft.com/en-us/"> ![Static Badge](https://img.shields.io/badge/.NET-url?style=for-the-badge&logoColor=lightblue&color=purple) </a>
* <a href="https://www.pgadmin.org/"> ![Static Badge](https://img.shields.io/badge/pgAdmin-url?style=for-the-badge&logo=postgresql&logoColor=white&labelColor=grey&color=grey) </a>
* <a href="https://reactnative.dev/">![Static Badge](https://img.shields.io/badge/react%20native-url?style=for-the-badge&logo=react&color=black)</a>
<!-- GETTING STARTED -->
<!-- Make sure to add appropriate information about what prerequisite technologies the user would need and also the steps to install your project on their own machines -->
## Getting Started

The following instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Ensure that you have the latest version of [Electron](https://www.electronjs.org/) installed on your machine. 

### How to install

### Installation
Here are a couple of ways to clone this repo:

1. Software </br>
`GitHub Desktop` -> `File` -> `Clone repository` -> `URL`</br>
Enter `https://github.com/DieterR97/Astro.git` into the URL field and press the `Clone` button.

2. Clone Repository </br>
Run the following in the command-line to clone the project:
   ```sh
   git clone https://github.com/DieterR97/Astro.git
   ```
    Open `GitHub Desktop` and select `File | Open...` from the menu. Select cloned directory and press `Open in Visual Studio Code` button

3. Run backend server </br>
Run the following in the backend command-line:
   ```sh
   Run without debugging
   ```

4. Run front-end server </br>
Get front-end at [https://github.com/DieterR97/Astro](https://github.com/DieterR97/Astro)
   ```sh
   npm run dev
   ```


<!-- FEATURES AND FUNCTIONALITY-->
<!-- You can add the links to all of your imagery at the bottom of the file as references -->
## Features and Functionality

![Authentication](astro-backend/assets/Authentication.jpg)
### Authentication

The account holders user must create an account/profile so that they can log on to and out of the app. Administering users should be able to log on to the system. Both the client and the admin users will need to be authenticated with 2-Factor authentication.

![Account](astro-backend/assets/Overview.jpg)
### Accounts

The account holders user should be able to view and edit their bank account information, view their account balance and status, and view a summary of their transactions. Their account’s status level is based on the amount of tokens, which has allows for better interest rates and lower transaction fees.

![Purchasings](astro-backend/assets/Purchasing_Currency.jpg)
### Purchasing

Users should be able to purchase one currency. Users should be able to display current exchange rates of the available currency and confirm the purchase and update the user's balance accordingly.

![Transactions](astro-backend/assets/Transactions.jpg)
### Transactions

The account holders user should be able to see all of their past transactions as well as perform new transactions. These transactions include the purchasing of new tokens, the withdrawal of tokens and the transferring of tokens.

![Admin Portal](astro-backend/assets/Admin_Portal.jpg)
### Admin Portal

Admin users should be able to view all of the account holders and their respective transaction history. The admin user should also be able to freeze an account.

<!-- CONCEPT PROCESS -->
<!-- Briefly explain your concept ideation process -->
## Concept Process

The `Conceptual Process` is the set of actions, activities and research that was done when starting this project.

### Ideation

![Moodboard](astro-backend\assets\Moodboard.jpg)

### Wireframes

![Wireframe1](astro-backend\assets\Wireframe1.jpg)![Wireframe2](astro-backend\assets\Wireframe2.jpg)![Wireframe3](astro-backend\assets\Wireframe3.jpg)![Wireframe4](astro-backend\assets\Wireframe4.jpg)![Wireframe5](astro-backend\assets\Wireframe5.jpg)![Wireframe6](astro-backend\assets\Wireframe6.jpg)![Wireframe7](astro-backend\assets\Wireframe7.jpg)![Wireframe8](astro-backend\assets\Wireframe8.jpg)![Wireframe9](astro-backend\assets\Wireframe9.jpg)![Wireframe10](astro-backend\assets\Wireframe10.jpg)

### ER Diagram

![ER Diagram](astro-backend\assets\ER%20Diagram.jpg)

### User-flow

![User Flow](astro-backend\assets\User%20Flow.jpg)

<!-- DEVELOPMENT PROCESS -->
## Development Process

The `Development Process` is the technical implementations and functionality done in the frontend and backend of the application.

### Implementation Process
<!-- stipulate all of the functionality you included in the project -->

* Made use of both `C#` and `Electron` to create a pet clothing and accessory inventory management system. 
* Made use of `Hugeicons.pro` icons.

#### Highlights
<!-- stipulated the highlight you experienced with the project -->
* Achieved 90% project completion.
* Implemented a unique and elegant design.

#### Challenges
<!-- stipulated the challenges you faced with the project and why you think you faced it or how you think you'll solve it (if not solved) -->
* Difficulty in enabling SendGrid account.

### Future Implementation
<!-- stipulate functionality and improvements that can be implemented in the future. -->

* Create a SendGrid account ahead of beginning the project. 

<!-- MOCKUPS -->
## Final Outcome

### Mockups

![Mockup2](astro-backend\assets\mockup_two.png)
<br>
![Mockup3](astro-backend\assets\mockup_three.jpg)

<!-- VIDEO DEMONSTRATION -->
### Video Demonstration

To see a run through of the application, click below:

[View Demonstration](https://drive.google.com/file/d/1v8MVrl1g98yQJLvkuEm_BxsAA0NvnWFn/view?usp=sharing)

<!-- CONTRIBUTING -->
## Contributing

Contributions are what makes the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/Astro`)
3. Commit your Changes (`git commit -m 'Add some Astro'`)
4. Push to the Branch (`git push origin feature/Astro`)
5. Open a Pull Request

<!-- AUTHORS -->
## Authors

* **Dieter Roelofse** - [DieterR97](https://github.com/DieterR97)
* **Erik Conradie** - [EConradie](https://github.com/EConradie)
* **Ungerer Hattingh** - [Ungerer221](https://github.com/Ungerer221)
* **Mariné Du Plessis** - [DupieM](https://github.com/DupieM)
* **Hannah Naidoo** - [HannahAmaria](https://github.com/HannahAmaria)

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.\

<!-- LICENSE -->
## Contact

* **Dieter Roelofse** - [221122@virtualwindow.co.za](221122@virtualwindow.co.za)
* **Erik Conradie** - [221147@virtualwindow.co.za](221147@virtualwindow.co.za)
* **Ungerer Hattingh** - [221302@virtualwindow.co.za](221302@virtualwindow.co.za)
* **Mariné Du Plessis** - [221326@virtualwindow.co.za](221326@virtualwindow.co.za)
* **Hannah Naidoo** - [21100366@virtualwindow.co.za](21100366@virtualwindow.co.za)
* **Project Link** - https://github.com/DieterR97/Astro

<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
<!-- all resources that you used and Acknowledgements here -->
* [Hugeicon.pro](https://hugeicons.com/)
* [ChatGPT](https://chatgpt.com/)

<!-- MARKDOWN LINKS & IMAGES -->
[image1]: /path/to/image.png
[image2]: /path/to/image.png
[image3]: /path/to/image.png
[image4]: /path/to/image.png
[image5]: /path/to/image.png
[image6]: /path/to/image.png
[image7]: /path/to/image.png
[image8]: /path/to/image.png
[image9]: /path/to/image.png
[image10]: /path/to/image.png


<!-- Refer to https://shields.io/ for more information and options about the shield links at the top of the ReadMe file -->
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/nameonlinkedin/
[instagram-shield]: https://img.shields.io/badge/-Instagram-black.svg?style=flat-square&logo=instagram&colorB=555
[instagram-url]: https://www.instagram.com/instagram_handle/
[behance-shield]: https://img.shields.io/badge/-Behance-black.svg?style=flat-square&logo=behance&colorB=555
[behance-url]: https://www.behance.net/name-on-behance/

