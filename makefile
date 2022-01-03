NAME	=	labyrinth

FOLDER	=	src/

SRC     =	game.cs				\
			enum.cs				\
			input_manager.cs	\
			level.cs			\
			IScene.cs			\
			Vertex.cs			\
			graph.cs			\
			menu_scene.cs		\
			game_scene.cs

all: $(NAME)

$(NAME):
	mcs $(addprefix $(FOLDER), $(SRC)) -out:$(NAME)

clean:
	$(RM) $(NAME)

re: clean all