# MicroservicesInsurancePolicy

# MicroservicesInsurancePolicy

## Overview
This repository contains two microservices: `PersonMicroService` and `InsuranceMicroService`. These services manage personal data and insurance policies, respectively, and can communicate with each other.

## Services

### Person Microservice
This service handles CRUD operations for person data.

- **Fields**: `PersonId`, `FirstName`, `LastName`, `BirthDate`, `Email`
- **Endpoints**:
  - `GET /Person/{id}`
  - `GET /Person`
  - `POST /Person`
  - `PUT /Person/{id}`
  - `DELETE /Person/{id}`

### Insurance Microservice
This service manages insurance policies linked to persons.

- **Fields**: `PolicyId`, `PersonId`, `PolicyType`, `StartDate`, `EndDate`, `PremiumAmount`
- **Endpoints**:
  - `GET /Insurance/{id}`
  - `GET /Insurance`
  - `POST /Insurance`

## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Running the Services

1. **Clone the repository:**
    ```sh
    git clone https://github.com/yourusername/MicroservicesInsurancePolicy.git
    cd MicroservicesInsurancePolicy
    ```

2. **Build the Solution:**
    Open the solution in Visual Studio and build it, or use the following command:
    ```sh
    dotnet build
    ```

3. **Run the Services:**
    - For `PersonMicroService.Api`:
        ```sh
        cd PersonMicroService.Api
        dotnet run
        ```
    - For `InsuranceMicroService.Api`:
        ```sh
        cd InsuranceMicroService.Api
        dotnet run
        ```

4. **Run the Tests:**
    - For `PersonMicroserviceTests`:
        ```sh
        cd PersonMicroserviceTests
        dotnet test
        ```
    - For `InsuranceMicroserviceTests`:
        ```sh
        cd InsuranceMicroserviceTests
        dotnet test
        ```

### Usage Instructions

1. **Start the Microservices:**
    Ensure both microservices are running.

2. **Use Swagger UI:**
    Open your browser and navigate to `https://localhost:7175/swagger/index.html` for the Insurance Microservice API documentation.

3. **Creating a Person:**
    - Use the `POST /Person` endpoint to create a new person.
    - Copy the `PersonId` from the response.

4. **Creating an Insurance Policy:**
    - Use the `POST /Insurance` endpoint.
    - Include the `PersonId` from the previous step in the request body.

## Project Structure
- `InsuranceMicroService.Api`: Contains the API implementation for managing insurance policies.
- `InsuranceMicroserviceTests`: Contains unit tests for the Insurance microservice.
- `PersonMicroService.Api`: Contains the API implementation for managing person data.
- `PersonMicroserviceTests`: Contains unit tests for the Person microservice.

## Contact
For any questions or issues, please contact seifvand@gmail.com

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
